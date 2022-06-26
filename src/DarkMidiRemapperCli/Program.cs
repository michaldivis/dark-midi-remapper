using DarkMidiRemapperCore;
using NAudio.Midi;

var sourceMidiFilePath = @"E:\Projects\DarkMidiRemapper\assets\test_drums.mid";
var targetMidiFilePath = @"C:\Users\Michal\Downloads\test_drums_remapped.mid";
var mf = new MidiFile(sourceMidiFilePath, false);

var remapper = new Remapper();
var mappings = remapper.GetMappings(mf);

//TODO assign names
mappings[0].Name = "Kick"; //TODO remove this
mappings[1].Name = "Snare"; //TODO remove this

//TODO assign target notes
mappings[0].TargetNote = MidiNote.Find(MidiNumber.C2);
mappings[1].TargetNote = MidiNote.Find(MidiNumber.D2);

remapper.AlterMapping(mf, mappings);

MidiFile.Export(targetMidiFilePath, mf.Events);