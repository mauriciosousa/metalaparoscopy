// Copyright Â© 2018, Meta Company.  All rights reserved.
// 
// Redistribution and use of this software (the "Software") in source and binary forms, with or 
// without modification, is permitted provided that the following conditions are met:
// 
// 1.      Redistributions in source code must retain the above copyright notice, this list of 
//         conditions and the following disclaimer.
// 2.      Redistributions in binary form must reproduce the above copyright notice, this list of 
//         conditions and the following disclaimer in the documentation and/or other materials 
//         provided with the distribution.
// 3.      The name of Meta Company (â€œMetaâ€) may not be used to endorse or promote products derived 
//         from this software without specific prior written permission from Meta.
// 4.      LIMITATION TO META PLATFORM: Use of the Software and of any and all libraries (or other 
//         software) incorporating the Software (in source or binary form) is limited to use on or 
//         in connection with Meta-branded devices or Meta-branded software development kits.  For 
//         example, a bona fide recipient of the Software may modify and incorporate the Software 
//         into an application limited to use on or in connection with a Meta-branded device, while 
//         he or she may not incorporate the Software into an application designed or offered for use 
//         on a non-Meta-branded device.
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDER "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, 
// INCLUDING, BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A 
// PARTICULAR PURPOSE ARE DISCLAIMED.  IN NO EVENT SHALL META COMPANY BE LIABLE FOR ANY DIRECT, 
// INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED 
// TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA OR PROFITS; OR BUSINESS 
// INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT 
// LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS 
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
using UnityEngine;

namespace Meta.Examples
{
    public class GazeScript : MetaBehaviour, IGazeStartEvent, IGazeEndEvent
    {
		public GameObject Quad;

        private bool _bIsGazing;
		private float time = 0.0f;

		private Vector3 retro = new Vector3(0.03f, 0.06f, 0.55f);
		private Vector3 fov = new Vector3(0.019f, 0.0f, 0.125f);

		private Color originalColor;

		private void Start()
		{
			originalColor = GetComponent<Renderer>().material.color;

			if (this.name.Equals ("Free"))
				GetComponent<Renderer> ().material.color = Color.green;

		}

        private void Update()
        {
			ChangeQuadPosition ();
        }

        /// <summary>
        /// From the IGazeStartEvent interface. This occurs when the gaze gesture begins with this object as the subject.
        /// </summary>
        public void OnGazeStart()
        {
            _bIsGazing = true;
        }

        /// <summary>
        /// From the IGazeEndEvent interface. This occurs when the gaze gesture ends and this object is no longer the subject.
        /// </summary>
        public void OnGazeEnd()
        {
            _bIsGazing = false;
        }

		private void ChangeQuadPosition ()
		{
			time += Time.deltaTime;

			if (_bIsGazing) {

				time += Time.deltaTime;

				if (time >= 1.0f) {

					time = 0.0f;

					if (this.name.Equals ("Retro")) {
						Vector3 pos = Camera.main.transform.TransformPoint (retro);
						Quad.transform.position = pos;
						metaContext.Get<HudLock>().RemoveHudLockedObject(Quad.GetComponent<MetaLocking>());
						metaContext.Get<HudLock>().AddHudLockedObject(Quad.GetComponent<MetaLocking>());
						MetaLocking.canMove = false;
					} else if (this.name.Equals ("FOV")) {
						Vector3 pos = Camera.main.transform.TransformPoint (fov);
						Quad.transform.position = pos;
						metaContext.Get<HudLock>().RemoveHudLockedObject(Quad.GetComponent<MetaLocking>());
						metaContext.Get<HudLock>().AddHudLockedObject(Quad.GetComponent<MetaLocking>());
						MetaLocking.canMove = false;
					} else
						MetaLocking.canMove = true;

					ChangeMaterialColor ();
				}
			}
		}
			

		private void ChangeMaterialColor(){

			GameObject[] objects = GameObject.FindGameObjectsWithTag ("ViewOption");
			GetComponent<Renderer> ().material.color = Color.green;

			foreach (GameObject obj in objects) {
				if (!obj.name.Equals (this.name))
					obj.GetComponent<Renderer> ().material.color = originalColor;
			}

		}
        
    }
}
