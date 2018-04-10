using GL.Scripts.Battle.CharacterControllers;
using GL.Scripts.Battle.Systems;

namespace GL.Scripts.Battle.Commands.Implements
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
        /// 対象となるパーティタイプ
        /// </summary>
        Constants.TargetPartyType TargetPartyType { get; }

        /// <summary>
        /// ターゲットタイプ
        /// </summary>
        /// <remarks>
        /// 誰を狙うかを決めるのに必要
        /// </remarks>
        Constants.TargetType TargetType { get; }
        
        /// <summary>
        /// 対象の検索対象となるパラメータタイプ
        /// </summary>
        Constants.StatusParameterType TargetStatusParameterType { get; }

        /// <summary>
        /// コマンドタイプ
        /// </summary>
        Constants.CommandType CommandType { get; }

        /// <summary>
        /// コマンドを実行する
        /// </summary>
        /// <param name="invoker">実行するキャラクター</param>
        void Invoke(Character invoker);
    }
}
