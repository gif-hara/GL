using System.Collections.Generic;
using System.Linq;
using GL.Scripts.Battle.Commands.Impletents;

namespace GL.Scripts.Battle.CharacterControllers
{
    /// <summary>
    /// キャラクターのステータスを制御するクラス.
    /// </summary>
    public sealed class CharacterStatusController
    {
        public CharacterStatus Status { private set; get; }
        
        /// <summary>
        /// 使用可能なコマンド
        /// </summary>
        public IImplement[] Commands { private set; get; }

        /// <summary>
        /// 待機した量
        /// </summary>
        public float Wait { set; get; }

        /// <summary>
        /// 元となる設計図
        /// </summary>
        public Blueprint Blueprint{ private set; get; }

        /// <summary>
        /// ヒットポイント最大値
        /// </summary>
        public int HitPointMax { get { return this.Blueprint.Status.HitPoint; } }
        
        /// <summary>
        /// 死亡しているか返す
        /// </summary>
        public bool IsDead { get { return this.Status.HitPoint <= 0; } }

        public CharacterStatusController(Blueprint blueprint)
        {
            this.Blueprint = blueprint;
            this.Status = new CharacterStatus(this.Blueprint);
            this.Commands = this.Blueprint.Commands.Select(x => x.Create()).ToArray();
            this.Wait = 0.0f;
        }
    }
}
