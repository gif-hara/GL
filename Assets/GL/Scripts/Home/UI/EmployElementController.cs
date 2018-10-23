using GL.Database;
using GL.User;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

namespace GL.Home.UI
{
    /// <summary>
    /// 雇用リストの要素を制御するクラス
    /// </summary>
    public sealed class EmployElementController : MonoBehaviour
    {
        [SerializeField]
        private Text playerName;

        [SerializeField]
        private Text jobName;

        [SerializeField]
        private Button button;
        public Button Button => this.button;

        public EmployElementController Setup(CharacterRecord blueprint)
        {
            this.playerName.text = blueprint.CharacterName;
            this.jobName.text = blueprint.Job.JobName;

            return this;
        }
    }
}
