﻿using Android.Views;
using Android.Graphics;
using Android.Content;

namespace PruSign.Android
{
	public class DrawView : View
	{
		public DrawView(Context context) : base(context)
		{
			Start();
		}

		public Color CurrentLineColor { get; set; }
		public float PenWidth { get; set; }
		private Path DrawPath;
		private Paint DrawPaint;
		private Paint CanvasPaint;
		private Canvas DrawCanvas;
		private Bitmap CanvasBitmap;


		private void Start()
		{
			CurrentLineColor = Color.Black;
			PenWidth = 3.0f;

			DrawPath = new Path();
			DrawPaint = new Paint
			{
				Color = CurrentLineColor,
				AntiAlias = true,
				StrokeWidth = PenWidth
			};

			DrawPaint.SetStyle(Paint.Style.Stroke);
			DrawPaint.StrokeJoin = Paint.Join.Round;
			DrawPaint.StrokeCap = Paint.Cap.Round;

			CanvasPaint = new Paint
			{
				Dither = true
			};

		}


		protected override void OnSizeChanged(int w, int h, int oldw, int oldh)
		{
			base.OnSizeChanged(w, h, oldw, oldh);

			CanvasBitmap = Bitmap.CreateBitmap(w, h, Bitmap.Config.Argb8888);
			DrawCanvas = new Canvas(CanvasBitmap);
		}


		protected override void OnDraw(Canvas canvas)
		{
			base.OnDraw(canvas);

			DrawPaint.Color = CurrentLineColor;
			canvas.DrawBitmap(CanvasBitmap, 0, 0, CanvasPaint);
			canvas.DrawPath(DrawPath, DrawPaint);
		}

		public override bool OnTouchEvent(MotionEvent e)
		{
			var touchX = e.GetX();
			var touchY = e.GetY();

			var newPoint = new PointF(touchX, touchY);
			Droid.PointWhen customPoint = new Droid.PointWhen
			{
				point = newPoint,
				when = System.DateTime.Now.Ticks
			};
			points.Add(customPoint);

			switch (e.Action)
			{
				case MotionEventActions.Down:
					DrawPath.MoveTo(touchX, touchY);
					break;
				case MotionEventActions.Move:
					DrawPath.LineTo(touchX, touchY);
					break;
				case MotionEventActions.Up:
					DrawCanvas.DrawPath(DrawPath, DrawPaint);
					DrawPath.Reset();
					break;
				default:
					return false;
			}

			Invalidate();

			return true;
		}
	}
}