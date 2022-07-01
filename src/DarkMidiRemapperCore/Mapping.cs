using DarkMusicConcepts;

namespace DarkMidiRemapperCore;

public class Mapping
{
    public Note SourceNote { get; }
    public Note TargetNote { get; set; }
    public string? Name { get; set; }

    public Mapping(Note sourceNote)
    {
        SourceNote = sourceNote;
        TargetNote = sourceNote;
    }

    public override string ToString()
    {
        return $"{Name ?? "n/a"} - Source: {SourceNote}, Target: {TargetNote}";
    }
}
