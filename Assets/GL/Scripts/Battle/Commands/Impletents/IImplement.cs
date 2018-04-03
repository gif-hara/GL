using GL.Scripts.Battle.CharacterControllers;
using HK.GL.Battle;

namespace GL.Scripts.Battle.Commands.Impletents
{
    /// <summary>
    /// 実際にゲームで使用するコマンドのインターフェイス
    /// </summary>
    public interface IImplement
    {
        /// <summary>
        /// コマンド名
        /// </summary>
        string Name { get; }

        /// <summary>
        /// ターゲットタイプ
        /// </summary>
        /// <remarks>
        /// 誰を狙うかを決めるのに必要
        /// </remarks>
        Constants.TargetType TargetType { get; }

        /// <summary>
        /// コマンドを実行する
        /// </summary>
        /// <param name="invoker">実行するキャラクター</param>
        void Invoke(Character invoker);
    }
}
