using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Testing
{
    public class TriggerInformation : MonoBehaviour
    {
        /// <summary>
        /// Determines if the player has entered the trigger at least once
        /// </summary>
        public bool HasPlayerEnteredTrigger { get; private set; }

        /// <summary>
        /// Determines if the player is currently inside of the trigger
        /// </summary>
        public bool IsPlayerInTrigger { get; private set; }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                HasPlayerEnteredTrigger = true;
                IsPlayerInTrigger = true;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                IsPlayerInTrigger = false;
            }
        }
    }
}
