using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using Melanchall.DryWetMidi.Common;
using Melanchall.DryWetMidi.Composing;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using Melanchall.DryWetMidi.Multimedia;
using Melanchall.DryWetMidi.Standards;
using UnityEngine;

public class FileOpen : MonoBehaviour
{

    public string projectName;
    private string defaultFolder = "Assets/FileLibrary/";
    private string defaultFileName = "/track.mid";
    private MidiFile midiFile;
    private ICollection<TimedEvent> timedEvents;
    private ICollection<Note> notes;

    // Start is called before the first frame update
    void Start()
    {
        string filePath = defaultFolder + projectName + defaultFileName;
        midiFile = MidiFile.Read(filePath);
        IEnumerable<TrackChunk> trackChunks = midiFile.GetTrackChunks();
		TempoMap tempoMap = midiFile.GetTempoMap();
		var tc = tempoMap.GetTempoChanges();
		ValueChange<Tempo> t = tc.First();
		Tempo tempo = t.Value;
		double BPM = tempo.BeatsPerMinute;
		double BPN = tempo.MicrosecondsPerQuarterNote;
		Debug.Log("BPM: " + BPM);

        foreach(TrackChunk chunk in trackChunks)
        {
			#nullable enable
			string? trackName = GetTrackName(chunk); 
			#nullable disable

			if(trackName != null){

				/*
				Debug.Log("Track Name ######### " + trackName);

				if(trackName == "Pulse"){
					notes = chunk.GetNotes();
					foreach(Note note in notes)
					{
						Debug.Log(note.NoteName + " T:" + note.Time);
					}
				}

				
				if(trackName == "Hold"){
					
					notes = chunk.GetNotes();
					foreach(Note note in notes)
					{
						Debug.Log(note.NoteName + " T:" + note.Time + " L:" + note.Length);
					}

				}
				Debug.Log("---");
				*/

			} else {
				foreach(var e in chunk.Events){
					//Debug.Log(e.ToString());
				}
			}
        }



    }

    private string GetTrackName(TrackChunk chunk)
    {
        return chunk.Events.OfType<SequenceTrackNameEvent>().FirstOrDefault()?.Text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
