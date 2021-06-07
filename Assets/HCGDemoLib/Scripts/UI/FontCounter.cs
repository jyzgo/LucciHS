using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HCGDemo
{
    [RequireComponent(typeof(Text))]
    public class FontCounter : MonoBehaviour
    {

        // Start is called before the first frame update
        void Start()
        {
            _numTex = GetComponent<Text>();
        }

        Text _numTex;
        public float From = 0;
        public int To = 1000;
        public float speed = 1;
        public int multipler = 1;

        // Update is called once per frame
        void Update()
        {
            if (From < To)
            {
                From += speed;
            }
            else
            {
                From = To;
            }
            _numTex.text = (((int)From) * multipler).ToString();
        }
    }
}
