using DarkMusicConcepts;
using NAudio.Midi;

namespace DarkMidiRemapperCore;
public class Remapper
{
    private const int TrackNumber = 0; //TODO don't just use the first track number, check if there are more and do something about it

    public List<Mapping> GetMappings(MidiFile midiFile)
    {
        var noteEvents = midiFile.Events[TrackNumber]
            .Where(a => a is NoteEvent)
            .Cast<NoteEvent>()
            .ToList();

        var distinctNotes = noteEvents
            .Select(a => a.NoteNumber)
            .Distinct()
            .OrderBy(a => a)
            .ToList();

        var mappings = distinctNotes
            .Select(a => new Mapping(Note.FindByMidiNumber(a)))
            .ToList();

        return mappings;
    }

    public void AlterMapping(MidiFile midiFile, IEnumerable<Mapping> mappings)
    {
        foreach (var evnt in midiFile.Events[TrackNumber])
        {
            if(evnt is NoteEvent noteEvent)
            {
                var mapping = mappings.FirstOrDefault(a => a.SourceNote.MidiNumber.Value == noteEvent.NoteNumber);
                if (mapping is null)
                {
                    //TODO handle mapping not found
                    continue;
                }

                if(mapping.TargetNote is null)
                {
                    //TODO handle TargetNote not set
                    continue;
                }

                noteEvent.NoteNumber = mapping.TargetNote.MidiNumber.Value;
            }
        }
    }
}
