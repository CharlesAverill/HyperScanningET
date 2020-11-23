using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Looxid.Link;
using UnityEngine.PlayerLoop;
using System;
using System.IO;
using System.Text;
using Photon.Pun;
public class EEG : MonoBehaviourPunCallbacks, IPunObservable
{
    // Start is called before the first frame update
    public bool record = false;
    private string outputPath;
    void Start()
    {
        outputPath = @"./data.csv";
        LooxidLinkManager.Instance.Initialize();
    }
    // Update is called once per frame
    void Update()
    {
        //print("Accessing update method");
        // Keystroke to start and stop recording if the 'f' key
        if (Input.GetKeyDown("f"))
        {
            //print("Switch!!!!!!!!!");
            record = !record;
        }
    }
    void OnEnable()
    {
        LooxidLinkData.OnReceiveEEGRawSignals += OnReceiveEEGRawSignals;
    }
    void OnDisable()
    {
        LooxidLinkData.OnReceiveEEGRawSignals -= OnReceiveEEGRawSignals;
    }
    void OnReceiveEEGRawSignals(EEGRawSignal rawSignalData)
    {
        if (record)
        {
            if (!File.Exists(outputPath))
            {
                using (new FileStream(outputPath, FileMode.CreateNew)) { }
                string output = "timestamp,AF3,AF4,Fp1,Fp2,AF7,AF8,reference,ground\n";
                File.AppendAllText(outputPath, output);
            }
            Debug.Log("New data received!");
            for (int i = 0; i < rawSignalData.rawSignal.Count; i++)
            {
                string output = rawSignalData.rawSignal[i].timestamp.ToString();
                for (int j = 0; j < rawSignalData.rawSignal[i].ch_data.Length; j++)
                {
                    output += "," + rawSignalData.rawSignal[i].ch_data[j];
                }
                File.AppendAllText(outputPath, output + "\n");
            }
        }
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            //Debug.Log("Is Writing");
            stream.SendNext(record);
        }
        else
        {
            //Debug.Log("Is Reading");
            record = (bool)stream.ReceiveNext();
        }
    }
}