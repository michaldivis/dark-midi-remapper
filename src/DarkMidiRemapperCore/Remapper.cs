using NAudio.Midi;

namespace DarkMidiRemapperCore;
public class Remapper
{
    private const int TrackNumber = 0; //TODO don't just use the first track number, check if there are more and do something about it

    public List<MidiNote> GetDistinctNotes(MidiFile midiFile)
    {
        var distinctNotes = midiFile.Events[TrackNumber]
            .Where(a => a is NoteEvent)
            .Cast<NoteEvent>()
            .Select(a => a.NoteNumber)
            .Distinct()
            .OrderBy(a => a)
            .Select(a => MidiNote.FindByNumber(a))
            .ToList();

        return distinctNotes;
    }

    public void AlterMapping(MidiFile midiFile, List<Mapping> mappings)
    {
        foreach (var evnt in midiFile.Events[TrackNumber])
        {
            if(evnt is NoteEvent noteEvent)
            {
                var mapping = mappings.First(a => a.SourceNote.NoteNumber == noteEvent.NoteNumber);
                noteEvent.NoteNumber = mapping.TargetNote.NoteNumber;
            }
        }
    }
}
