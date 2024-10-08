
using System.Collections.Generic;
using System;

namespace YG
{
    [System.Serializable]
    public class SavesYG
    {
        // "Технические сохранения" для работы плагина (Не удалять)
        public int idSave;
        public bool isFirstSession = true;
        public string language = "ru";
        public bool promptDone;

        public int Level;
        public int Money;
        public int ActivatedSkillId;
        public List<Skill> Skills;
        public bool IsX2Purchased = false;
    }
}
