using System;
using System.IO;
using System.Reactive;
using System.Threading.Tasks;

namespace Grabacr07.KanColleViewer.Models.Cef
{
	public class ScreenshotRequest
	{
		private readonly TaskCompletionSource<Unit> source;
		private readonly string path;

		public string Id { get; }

		public ScreenshotRequest(string path, TaskCompletionSource<Unit> source)
		{
			this.Id = $"ssReq{DateTimeOffset.Now.Ticks}";
			this.path = path;
			this.source = source;
		}

		public void Complete(string dataUrl)
		{
			try
			{
				var array = dataUrl.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
				if (array.Length != 2) throw new Exception($"無効な形式: {dataUrl}");

				var base64 = array[1];
				var bytes = Convert.FromBase64String(base64);
				var extension = Path.GetExtension(this.path);

				if (extension == ".clip")
				{
					// クリップボードへ転送
					using (var ms = new MemoryStream(bytes))
					{
						ms.Seek(0, SeekOrigin.Begin);
						var bi = new System.Windows.Media.Imaging.BitmapImage();
						bi.BeginInit();
						bi.CacheOption = System.Windows.Media.Imaging.BitmapCacheOption.OnLoad;
						bi.StreamSource = ms;
						bi.EndInit();
						bi.Freeze();

						System.Windows.Clipboard.SetImage(bi);
					}
				}
				else
				{
					using (var fs = new FileStream(this.path, FileMode.CreateNew))
					{
						fs.Write(bytes, 0, bytes.Length);
					}
				}

				this.source.SetResult(Unit.Default);
			}
			catch (Exception ex)
			{
				this.source.SetException(ex);
			}
		}
	}
}
