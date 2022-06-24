namespace DarkMidiRemapperCore;

public class MidiNote
{
    private const int C0NoteNumber = 24;
    private static readonly List<MidiNote> AllNotes = GetAllNotes(C0NoteNumber);

    public int NoteNumber { get; }
    public string NoteName { get; }

    private MidiNote(int noteNumber, string noteName)
    {
        NoteNumber = noteNumber;
        NoteName = noteName;
    }

    public static MidiNote FindByNumber(int noteNumber)
    {
        return AllNotes.First(a => a.NoteNumber == noteNumber);
    }    

    private static List<MidiNote> GetAllNotes(int cZeroNoteNumber)
    {
        var notes = new List<MidiNote>();

        void AddNote(string name, int octave, int position)
        {
            notes.Add(new MidiNote(cZeroNoteNumber + octave * 12 + position, $"{name}{octave}"));
        }

        for (int octave = -2; octave <= 9; octave++)
        {
            AddNote("C", octave, 0);
            AddNote("C#", octave, 1);
            AddNote("D", octave, 2);
            AddNote("D#", octave, 3);
            AddNote("E", octave, 4);
            AddNote("F", octave, 5);
            AddNote("F#", octave, 6);
            AddNote("G", octave, 7);
            AddNote("G#", octave, 8);
            AddNote("A", octave, 9);
            AddNote("A#", octave, 10);
            AddNote("B", octave, 11);
        }

        return notes;
    }

    public override string ToString()
    {
        return NoteName;
    }
}
