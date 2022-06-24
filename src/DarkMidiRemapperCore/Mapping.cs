namespace DarkMidiRemapperCore;

public class Mapping
{
    public MidiNote SourceNote { get; }
    public MidiNote? TargetNote { get; set; }
    public string? Name { get; set; }

    public Mapping(MidiNote sourceNote)
    {
        SourceNote = sourceNote;
    }
}
