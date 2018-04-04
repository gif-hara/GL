using System.Collections.Generic;
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
        public List<IImplement> Commands { private set; get; }

        /// <summary>
        /// 待機した量
        /// </summary>
        public float Wait { set; get; }

        /// <summary>
        /// 元となるスペック
        /// </summary>
        public Blueprint BaseSpec{ private set; get; }

        /// <summary>
        /// ヒットポイント最大値
        /// </summary>
        public int HitPointMax { get { return this.BaseSpec.Status.HitPoint; } }
        
        /// <summary>
        /// 死亡しているか返す
        /// </summary>
        public bool IsDead { get { return this.Status.HitPoint <= 0; } }

        public CharacterStatusController(Blueprint baseSpec)
        {
            this.BaseSpec = baseSpec;
            this.Status = new CharacterStatus(this.BaseSpec);
            this.Commands = this.BaseSpec.Commands;
            this.Wait = 0.0f;
        }
    }
}
