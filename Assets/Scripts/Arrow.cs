using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModularBridgeSystem
{
    public class Arrow : MonoBehaviour
    {
        [SerializeField] private GameObject viewCorrect;
        [SerializeField] private GameObject viewWrong;

        public void IsCorrect(bool state)
        {
            viewCorrect.SetActive(state);
            viewWrong.SetActive(!state);
        }
    }
}
