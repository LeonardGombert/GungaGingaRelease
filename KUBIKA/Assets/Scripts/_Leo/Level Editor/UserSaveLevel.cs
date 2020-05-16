using Kubika.Saving;
using UnityEngine;

namespace Kubika.Game
{
    public class UserSaveLevel : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void UserSavedLevel()
        {
            SaveAndLoad.instance.SaveLevelFull(UIManager.instance.saveLevelName.text, true);
        }

        public void UserLoadLevel()
        {
            SaveAndLoad.instance.LoadLevel(UIManager.instance.playerLevelsDropdown.captionText.text, true);
        }
    }
}