using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Grabacr07.KanColleViewer.Composition;

namespace ResourceLogger
{
	[Export(typeof(IPlugin))]
	[Export(typeof(ITool))]
	[Export(typeof(IRequestNotify))]
	[ExportMetadata("Guid", "F1FFD100-735C-4342-8011-6D7E78CE8811")]
	[ExportMetadata("Title", "ResourceLogger")]
	[ExportMetadata("Description", "資材を記録します。")]
	[ExportMetadata("Version", "0.1")]
	[ExportMetadata("Author", "a32kita")]
	public class ResourceLogger : IPlugin, ITool, IRequestNotify
	{
		// 非公開フィールド
		private ResourceLoggerViewModel viewModel;
		

		// 公開プロパティ

		/// <summary>
		/// ツール名を取得します。
		/// </summary>
		string ITool.Name
		{
			get => "Resource Logger";
		}

		/// <summary>
		/// ツールのビューを取得します。
		/// </summary>
		Object ITool.View
		{
			get => new ResourceLoggerView() { DataContext = this.viewModel };
		}


		// 公開イベント

		/// <summary>
		/// 
		/// </summary>
		public event EventHandler<NotifyEventArgs> NotifyRequested;


		// コンストラクタ

		/// <summary>
		/// <see cref="ResourceLogger"/> クラスの新しいインスタンスを初期化します。
		/// </summary>
		public ResourceLogger()
		{
			this.viewModel = new ResourceLoggerViewModel();
		}


		// 公開メソッド
		
		/// <summary>
		/// このプラグインを初期化します。
		/// </summary>
		public void Initialize()
		{
			// 初期化時に呼び出されます
			// 実装なし
		}
	}
}
