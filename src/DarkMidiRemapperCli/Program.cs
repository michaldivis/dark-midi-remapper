using DarkMidiRemapperCore;
using NAudio.Midi;

var sourceMidiFilePath = @"E:\Projects\DarkMidiRemapper\assets\test_drums.mid";
var targetMidiFilePath = @"C:\Users\Michal\Downloads\test_drums_remapped.mid";
var mf = new MidiFile(sourceMidiFilePath, false);

var remapper = new Remapper();
var distinctNotes = remapper.GetDistinctNotes(mf);

var mappings = distinctNotes
    .Select(a => new Mapping(a))
    .ToList();

//TODO assign names
mappings[0].Name = "Kick"; //TODO remove this

//TODO assign target notes
mappings.ForEach(a => a.TargetNote = MidiNote.FindByNumber(12)); //TODO remove this

remapper.AlterMapping(mf, mappings);

MidiFile.Export(targetMidiFilePath, mf.Events);