using GL.Scripts.Battle.CharacterControllers;

namespace HK.GL.Battle
{
    /// <summary>
    /// コマンドたらしめるインターフェイス.
    /// </summary>
    public interface ICommand
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
        /// <param name="invoker">実行するヤーツ</param>
        void Invoke(Character invoker);
    }
}
