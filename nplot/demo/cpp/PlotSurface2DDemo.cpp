/*
 * NPlot - A charting library for .NET
 * 
 * PlotSurface2DDemo.cs
 * Copyright (C) 2003-2006 Matt Howlett, Jamie McQuay.
 * All rights reserved.
 * 
 * Redistribution and use in source and binary forms, with or without modification,
 * are permitted provided that the following conditions are met:
 * 
 * 1. Redistributions of source code must retain the above copyright notice, this
 *    list of conditions and the following disclaimer.
 * 2. Redistributions in binary form must reproduce the above copyright notice,
 *    this list of conditions and the following disclaimer in the documentation
 *    and/or other materials provided with the distribution.
 * 3. Neither the name of NPlot nor the names of its contributors may
 *    be used to endorse or promote products derived from this software without
 *    specific prior written permission.
 * 
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
 * ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE DISCLAIMED.
 * IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR ANY DIRECT,
 * INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING,
 * BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE,
 * DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
 * LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE
 * OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED
 * OF THE POSSIBILITY OF SUCH DAMAGE.
 */

#include "StdAfx.h"
#include "PlotSurface2DDemo.h"

namespace NPlotDemo {

	CPlotSurface2DDemo::CPlotSurface2DDemo(void)
	{
		InitializeComponent();
		
		plotSurface->Anchor =   
			System::Windows::Forms::AnchorStyles::Left  |
			System::Windows::Forms::AnchorStyles::Right |
			System::Windows::Forms::AnchorStyles::Top   |
			System::Windows::Forms::AnchorStyles::Bottom;

			//List here the plot routines that you want to be accessed
			PlotRoutines = gcnew array<PlotDemoDelegate^> {
				gcnew PlotDemoDelegate(this,&NPlotDemo::CPlotSurface2DDemo::PlotWave),
				gcnew PlotDemoDelegate(this,&NPlotDemo::CPlotSurface2DDemo::PlotDataSet),
				gcnew PlotDemoDelegate(this,&NPlotDemo::CPlotSurface2DDemo::PlotMockup),
				gcnew PlotDemoDelegate(this,&NPlotDemo::CPlotSurface2DDemo::PlotImage),
				gcnew PlotDemoDelegate(this,&NPlotDemo::CPlotSurface2DDemo::PlotQE),
				gcnew PlotDemoDelegate(this,&NPlotDemo::CPlotSurface2DDemo::PlotLogAxis),
				gcnew PlotDemoDelegate(this,&NPlotDemo::CPlotSurface2DDemo::PlotLogLog),
				gcnew PlotDemoDelegate(this,&NPlotDemo::CPlotSurface2DDemo::PlotParticles),
				gcnew PlotDemoDelegate(this,&NPlotDemo::CPlotSurface2DDemo::PlotWavelet),
				gcnew PlotDemoDelegate(this,&NPlotDemo::CPlotSurface2DDemo::PlotSincFunction),
				gcnew PlotDemoDelegate(this,&NPlotDemo::CPlotSurface2DDemo::PlotGaussian),
				gcnew PlotDemoDelegate(this,&NPlotDemo::CPlotSurface2DDemo::PlotLabelAxis),
				gcnew PlotDemoDelegate(this,&NPlotDemo::CPlotSurface2DDemo::PlotCircular),
				gcnew PlotDemoDelegate(this,&NPlotDemo::CPlotSurface2DDemo::PlotCandleSimple),
				gcnew PlotDemoDelegate(this,&NPlotDemo::CPlotSurface2DDemo::PlotABC)
				};

			// set up printer
			printDocument = gcnew Printing::PrintDocument();
			printDocument->PrintPage += gcnew Printing::PrintPageEventHandler(this,&NPlotDemo::CPlotSurface2DDemo::pd_PrintPage);
			int id = currentPlot + 1;
			exampleNumberLabel->Text = "Plot " + id.ToString("0") + "/" + PlotRoutines->Length.ToString("0");

			plotSurface->RightMenu = NPlot::Windows::PlotSurface2D::DefaultContextMenu;

			// draw the first plot.
			currentPlot = 0;
			PlotRoutines[currentPlot]();
	}

	/// <summary>
	/// callback for quit button click
	/// </summary>
	/// <param name="sender">unused</param>
	/// <param name="e">unused</param>
	System::Void CPlotSurface2DDemo::quitButton_Click(System::Object^  sender, System::EventArgs^  e)
	{
		Close();
	}

	/// <summary>
	/// callback for print button click
	/// </summary>
	/// <param name="sender">unused</param>
	/// <param name="e">unused</param>
	System::Void CPlotSurface2DDemo::printButton_Click(System::Object^  sender, System::EventArgs^  e)
	{
		PrintDialog^ dlg = gcnew PrintDialog();
		dlg->Document = printDocument;
		if (dlg->ShowDialog() == System::Windows::Forms::DialogResult::OK) 
		{
			try
			{
				printDocument->Print();
			}
			catch(System::Exception^ ex)
			{
				Console::WriteLine( System::String::Format("problem printing - {0}\n",ex->ToString()) );
			}
		}	
	}

	/// <summary>
	/// callback for next button click
	/// </summary>
	/// <param name="sender">unused</param>
	/// <param name="e">unused</param>
	System::Void CPlotSurface2DDemo::nextPlotButton_Click(System::Object^  sender, System::EventArgs^  e)
	{
			currentPlot += 1;
			if (currentPlot == PlotRoutines->Length)
			{
				currentPlot = 0;
			}

			int id = currentPlot+1;
			qeExampleTimer->Enabled = false;
			exampleNumberLabel->Text = "Plot " + id.ToString("0") + "/" + PlotRoutines->Length.ToString("0");
			plotSurface->DateTimeToolTip = false;
			PlotRoutines[currentPlot]();
	}

	/// <summary>
	/// Callback for prev button click.
	/// </summary>
	/// <param name="sender">unused</param>
	/// <param name="e">unused</param>
	System::Void CPlotSurface2DDemo::prevPlotButton_Click(System::Object^  sender, System::EventArgs^  e)
	{
		currentPlot--;
		if( currentPlot == -1 )
		{
			currentPlot = PlotRoutines->Length-1;
		}

		int id = currentPlot + 1;
		exampleNumberLabel->Text = "Plot " + id.ToString("0") + "/" + PlotRoutines->Length.ToString("0");
		PlotRoutines[currentPlot]();
	}

	/// <summary>
	/// The PrintPage event is raised for each page to be printed.
	/// </summary>
	/// <param name="sender">unused</param>
	/// <param name="ev">unused</param>
	System::Void CPlotSurface2DDemo::pd_PrintPage(System::Object^ sender, Printing::PrintPageEventArgs^ ev) 
	{
		// The windows.forms PlotSurface2D control can also be 
		// rendered to other Graphics surfaces. Here we output to a
		// printer. 
		plotSurface->Draw( ev->Graphics, ev->MarginBounds );
		ev->HasMorePages = false;
	}

	/// <summary>
	/// Callback for QE example timer tick.
	/// </summary>
	/// <param name="sender">unused</param>
	/// <param name="e">unused</param>
	System::Void CPlotSurface2DDemo::qeExampleTimer_Tick(System::Object^  sender, System::EventArgs^  e)
	{
		Random^ r = gcnew Random();

		for (int i=0; i<PlotQEExampleValues->Length; ++i)
		{
			PlotQEExampleValues[i] = 8.0f + 12.0f * (double)r->Next(10000) / 10000.0f;
			if ( PlotQEExampleValues[i] > 18.0f )
			{
				PlotQEExampleTextValues[i] = "KCsTe";
			}
			else
			{
				PlotQEExampleTextValues[i] = "";
			}
		}

		plotSurface->Refresh();
	}
	

    #pragma region PlotCircular
    System::Void CPlotSurface2DDemo::PlotCircular()
	{
		array<System::String^>^ lines = gcnew array<System::String^>{
            "Circular Example. Demonstrates - ",
            "  * PiAxis, Horizontal and Vertical Lines.",
            "  * Placement of legend" };

        infoBox->Lines = lines;
        
        plotSurface->Clear();
		plotSurface->Add( gcnew HorizontalLine( 0.0, Color::LightGray ) );
		plotSurface->Add( gcnew VerticalLine( 0.0, Color::LightGray ) );

		const int N = 400;
		const double start = -Math::PI * 7.0;
		const double end = Math::PI * 7.0;

		array<double>^ xs = gcnew array<double>(N);
		array<double>^ ys = gcnew array<double>(N);

		for (int i=0; i<N; ++i) 
		{
			double t = ((double)i*(end - start)/(double)N + start);
			xs[i] = 0.5 * (t - 2.0 * Math::Sin(t));
			ys[i] = 2.0 * (1.0 - 2.0 * Math::Cos(t));
		}

		LinePlot^ lp = gcnew LinePlot( ys, xs );
		lp->Pen = gcnew Pen( Color::DarkBlue, 2.0f );
		lp->Label = "Circular Line"; // no legend, but still useful for copy data to clipboard.
		plotSurface->Add( lp );

		plotSurface->XAxis1 = gcnew PiAxis( plotSurface->XAxis1 );

		plotSurface->SmoothingMode = System::Drawing::Drawing2D::SmoothingMode::AntiAlias;

        
		plotSurface->Legend = gcnew Legend();
		plotSurface->Legend->AttachTo(PlotSurface2D::XAxisPosition::Bottom, PlotSurface2D::YAxisPosition::Right);
		plotSurface->Legend->HorizontalEdgePlacement = Legend::Placement::Inside;
		plotSurface->Legend->VerticalEdgePlacement = Legend::Placement::Inside;
        plotSurface->Legend->XOffset = -10;
        plotSurface->Legend->YOffset = -10;

        plotSurface->Refresh();
    }
    #pragma endregion
	#pragma region PlotWavelet
	System::Void CPlotSurface2DDemo::PlotWavelet()
	{
        array<System::String^>^ lines = gcnew array<System::String^>{
            "Wavelet Example. Demonstrates - ",
            "  * Reversing axes, setting number of tick marks on axis explicitly." };

        infoBox->Lines = lines;
        
        plotSurface->Clear();

		// Create a gcnew line plot from array data via the ArrayAdapter class.
		LinePlot^ lp = gcnew LinePlot();
		lp->DataSource = makeDaub(256);
		lp->Color = Color::Green;
		lp->Label = "Daubechies Wavelet"; // no legend, but still useful for copy data to clipboard.

		Grid^ myGrid = gcnew Grid();
		myGrid->VerticalGridType = Grid::GridType::Fine;
		myGrid->HorizontalGridType = Grid::GridType::Coarse;
		plotSurface->Add(myGrid);

		// And add it to the plot surface
		plotSurface->Add( lp );
		plotSurface->Title = "Reversed / Upside down Daubechies Wavelet";

		// Ok, the above will produce a decent default plot, but we would like to change
		// some of the Y Axis details. First, we'd like lots of small ticks (10) between 
		// large tick values. Secondly, we'd like to draw a grid for the Y values. To do 
		// this, we create a gcnew LinearAxis (we could also use Label, Log etc). Rather than
		// starting from scratch, we use the constructor that takes an existing axis and
		// clones it (values in the superclass Axis only are cloned). PlotSurface2D
		// automatically determines a suitable axis when we add plots to it (merging
		// current requirements with old requirements), and we use this as our starting
		// point. Because we didn't specify which Y Axis we are using when we added the 
		// above line plot (there is one on the left - YAxis1 and one on the right - YAxis2)
		// PlotSurface2D.Add assumed we were using YAxis1-> So, we create a gcnew axis based on
		// YAxis1, update the details we want, then set the YAxis1 to be our updated one.
		LinearAxis^ myAxis = gcnew LinearAxis( plotSurface->YAxis1 );
		myAxis->NumberOfSmallTicks = 2;
		plotSurface->YAxis1 = myAxis;

		// We would also like to modify the way in which the X Axis is printed. This time,
		// we'll just modify the relevant PlotSurface2D Axis directly. 
		plotSurface->XAxis1->WorldMax = 100.0f;
	
		plotSurface->PlotBackColor = Color::OldLace;
		plotSurface->XAxis1->Reversed = true;
		plotSurface->YAxis1->Reversed = true;
	
		// Force a re-draw of the control. 
		plotSurface->Refresh();
	}


	array<float>^ CPlotSurface2DDemo::makeDaub( int len )
	{
		array<float>^ daub4_h = gcnew array<float>
		{ 0.482962913145f, 0.836516303737f, 0.224143868042f, -0.129409522551f };

		array<float>^ daub4_g = gcnew array<float> 
		{ -0.129409522551f, -0.224143868042f, 0.836516303737f, -0.482962913145f };

		array<float>^ a = gcnew array<float>(len);
		a[8] = 1.0f;
		array<float>^ t = gcnew array<float>(len);

		int ns = 4;  // number smooth
		while ( ns < len/2 ) 
		{
			t = (array<float>^)a->Clone();

			ns *= 2;

			for ( int i=0; i<(ns*2); ++i ) 
			{
				a[i] = 0.0f;
			}

			// wavelet contribution
			for ( int i=0; i<ns; ++i ) 
			{
				for ( int j=0; j<4; ++j ) 
				{
					a[(2*i+j)%(2*ns)] += daub4_g[j] * t[i+ns];
				}
			}
			// smooth contribution
			for ( int i=0; i<ns; ++i ) 
			{
				for ( int j=0; j<4; ++j ) 
				{
					a[(2*i+j)%(2*ns)] += daub4_h[j]*t[i];
				}
			}
		}
		return a;
	}
	#pragma endregion
	#pragma region PlotLogAxis
	System::Void CPlotSurface2DDemo::PlotLogAxis()
	{
        array<System::String^>^ lines = gcnew array<System::String^>{
            "Log Example. Demonstrates - ",
            "  * How to chart data against log axes and linear axes at the same time."};

        infoBox->Lines = lines;
        
        plotSurface->Clear();

		// draw a fine grid. 
		Grid^ fineGrid = gcnew Grid();
		fineGrid->VerticalGridType = Grid::GridType::Fine;
		fineGrid->HorizontalGridType = Grid::GridType::Fine;
		plotSurface->Add( fineGrid );

		const int npt = 101;
		array<float>^ x = gcnew array<float>(npt);
		array<float>^ y = gcnew array<float>(npt);
		float step = 0.1f;
		for (int i=0; i<npt; ++i)
		{
			x[i] = i*step - 5.0f;
			y[i] = (float)Math::Pow( 10.0, x[i] );
		}
		float xmin = x[0];
		float xmax = x[npt-1];
		float ymin = (float)Math::Pow( 10.0, xmin );
		float ymax = (float)Math::Pow( 10.0, xmax );

		LinePlot^ lp = gcnew LinePlot();
		lp->OrdinateData = y;
		lp->AbscissaData = x;
		lp->Pen = gcnew Pen( Color::Red );
		plotSurface->Add( lp );

		LogAxis^ loga = gcnew LogAxis( plotSurface->YAxis1 );
		loga->WorldMin = ymin;
		loga->WorldMax = ymax;
		loga->AxisColor = Color::Red;
		loga->LabelColor = Color::Red;
		loga->TickTextColor = Color::Red;
		loga->LargeTickStep = 1.0f;
		loga->Label = "10^x";
		plotSurface->YAxis1 = loga;

		LinePlot^ lp1 = gcnew LinePlot();
		lp1->OrdinateData = y;
		lp1->AbscissaData = x;
		lp1->Pen = gcnew Pen( Color::Blue );
		plotSurface->Add( lp1, PlotSurface2D::XAxisPosition::Bottom, PlotSurface2D::YAxisPosition::Right );
		LinearAxis^ lin = gcnew LinearAxis( plotSurface->YAxis2 );
		lin->WorldMin = ymin;
		lin->WorldMax = ymax;
		lin->AxisColor = Color::Blue;
		lin->LabelColor = Color::Blue;
		lin->TickTextColor = Color::Blue;
		lin->Label = "10^x";
		plotSurface->YAxis2 = lin;

		LinearAxis^ lx = (LinearAxis^)plotSurface->XAxis1;
		lx->WorldMin = xmin;
		lx->WorldMax = xmax;
		lx->Label = "x";

		//((LogAxis)plotSurface->YAxis1).LargeTickStep = 2;

		plotSurface->Title = "Mixed Linear/Log Axes";

		plotSurface->Refresh();
	}
	#pragma endregion
	#pragma region PlotLogLog
	System::Void CPlotSurface2DDemo::PlotLogLog()
	{
        array<System::String^>^ lines = gcnew array<System::String^>{
            "LogLog Example. Demonstrates - ",
            "  * How to chart data against log axes and linear axes at the same time."};

        infoBox->Lines = lines;
        
        // log log plot
		plotSurface->Clear();

		Grid^ mygrid = gcnew Grid();
		mygrid->HorizontalGridType = Grid::GridType::Fine;
		mygrid->VerticalGridType = Grid::GridType::Fine;
		plotSurface->Add(mygrid);

		int npt = 101;
		array<float>^ x = gcnew array<float>(npt);
		array<float>^ y = gcnew array<float>(npt);

		float step=0.1f;

		// plot a power law on the log-log scale
		for (int i=0; i<npt; ++i)
		{
			x[i] = (i+1)*step;
			y[i] = x[i]*x[i];
		}
		float xmin = x[0];
		float xmax = x[npt-1];
		float ymin = y[0];
		float ymax = y[npt-1];

		LinePlot^ lp = gcnew LinePlot();
		lp->OrdinateData = y;
		lp->AbscissaData = x; 
		lp->Pen = gcnew Pen( Color::Red );
		plotSurface->Add( lp );
		// axes
		// x axis
		LogAxis^ logax = gcnew LogAxis( plotSurface->XAxis1 );
		logax->WorldMin = xmin;
		logax->WorldMax = xmax;
		logax->AxisColor = Color::Red;
		logax->LabelColor = Color::Red;
		logax->TickTextColor = Color::Red;
		logax->LargeTickStep = 1.0f;
		logax->Label = "x";
		plotSurface->XAxis1 = logax;
		// y axis
		LogAxis^ logay = gcnew LogAxis( plotSurface->YAxis1 );
		logay->WorldMin = ymin;
		logay->WorldMax = ymax;
		logay->AxisColor = Color::Red;
		logay->LabelColor = Color::Red;
		logay->TickTextColor = Color::Red;
		logay->LargeTickStep = 1.0f;
		logay->Label = "x^2";
		plotSurface->YAxis1 = logay;

		LinePlot^ lp1 = gcnew LinePlot();
		lp1->OrdinateData = y;
		lp1->AbscissaData = x;
		lp1->Pen = gcnew Pen( Color::Blue );
		plotSurface->Add( lp1, PlotSurface2D::XAxisPosition::Top, PlotSurface2D::YAxisPosition::Right );
		// axes
		// x axis (lin)
		LinearAxis^ linx = (LinearAxis^) plotSurface->XAxis2;
		linx->WorldMin = xmin;
		linx->WorldMax = xmax;
		linx->AxisColor = Color::Blue;
		linx->LabelColor = Color::Blue;
		linx->TickTextColor = Color::Blue;
		linx->Label = "x";
		plotSurface->XAxis2 = linx;
		// y axis (lin)
		LinearAxis^ liny = (LinearAxis^) plotSurface->YAxis2;
		liny->WorldMin = ymin;
		liny->WorldMax = ymax;
		liny->AxisColor = Color::Blue;
		liny->LabelColor = Color::Blue;
		liny->TickTextColor = Color::Blue;
		liny->Label = "x^2";
		plotSurface->YAxis2 = liny;

		plotSurface->Title = "x^2 plotted with log(red)/linear(blue) axes";

		plotSurface->Refresh();
	}
	#pragma endregion
	#pragma region PlotSincFunction
	System::Void CPlotSurface2DDemo::PlotSincFunction() 
	{
        array<System::String^>^ lines = gcnew array<System::String^>{
            "Sinc Function Example. Demonstrates - ",
            "  * Charting line and point plot at the same time.",
            "  * Adding a legend." };

        infoBox->Lines = lines;
        
        plotSurface->Clear(); // clear everything. reset fonts. remove plot components etc.

		System::Random^ r = gcnew Random();
		array<double>^ a = gcnew array<double>(100);
		array<double>^ b = gcnew array<double>(100);
		double mult = 0.00001f;
		for( int i=0; i<100; ++i )  
		{
			a[i] = ((double)r->Next(1000)/5000.0f-0.1f)*mult;
			if (i == 50 ) { b[i] = 1.0f*mult; } 
			else
			{
				b[i] = (double)Math::Sin((((double)i-50.0f)/4.0f))/(((double)i-50.0f)/4.0f);
				b[i] *= mult;
			}
			a[i] += b[i];
		}
	
		Marker^ m = gcnew Marker(Marker::MarkerType::Cross1,6,gcnew Pen(Color::Blue,2.0F));
		PointPlot^ pp = gcnew PointPlot( m );
		pp->OrdinateData = a;
		pp->AbscissaData = gcnew StartStep( -500.0, 10.0 );
		pp->Label = "Random";
		plotSurface->Add(pp); 

		LinePlot^ lp = gcnew LinePlot();
		lp->OrdinateData = b;
		lp->AbscissaData = gcnew StartStep( -500.0, 10.0 );
		lp->Pen = gcnew Pen( Color::Red, 2.0f );
		plotSurface->Add(lp);

		plotSurface->Title = "Sinc Function";
		plotSurface->YAxis1->Label = "Magnitude";
		plotSurface->XAxis1->Label = "Position";

		Legend^ legend = gcnew Legend();
		legend->AttachTo( PlotSurface2D::XAxisPosition::Top, PlotSurface2D::YAxisPosition::Left );
		legend->VerticalEdgePlacement = Legend::Placement::Inside;
		legend->HorizontalEdgePlacement = Legend::Placement::Inside;
        legend->YOffset = 8;

        plotSurface->Legend = legend;
		plotSurface->LegendZOrder = 1; // default zorder for adding idrawables is 0, so this puts legend on top.

		plotSurface->Refresh();
	}
	#pragma endregion
	#pragma region PlotGaussian
	System::Void CPlotSurface2DDemo::PlotGaussian()
	{
        array<System::String^>^ lines = gcnew array<System::String^>{
            "Gaussian Example. Demonstrates - ",
            "  * HistogramPlot and LinePlot." };

        infoBox->Lines = lines;
        
        plotSurface->Clear();

		System::Random^ r = gcnew Random();
		
		int len = 35;
		array<double>^ a = gcnew array<double>(len);
		array<double>^ b = gcnew array<double>(len);

		for (int i=0; i<len; ++i) 
		{
			int j = len-1-i;
			a[i] = (double)Math::Exp(-(double)(i-len/2)*(double)(i-len/2)/50.0f);
			b[i] = a[i] + (r->Next(10)/50.0f)-0.05f;
			if (b[i] < 0.0f) 
			{
				b[i] = 0;
			}
		}

		HistogramPlot^ sp = gcnew HistogramPlot();
		sp->DataSource = b;
		sp->Pen = Pens::DarkBlue;
		sp->Filled = true;
		sp->RectangleBrush = gcnew RectangleBrushes::HorizontalCenterFade( Color::Lavender, Color::Gold );
		sp->BaseWidth = 0.5f;
		sp->Label = "Random Data";
		LinePlot^ lp = gcnew LinePlot();
		lp->DataSource = a;
		lp->Pen = gcnew Pen( Color::Blue, 3.0f );
		lp->Label = "Gaussian Function";
		plotSurface->Add( sp );
		plotSurface->Add( lp );
		plotSurface->Legend = gcnew Legend();
		plotSurface->YAxis1->WorldMin = 0.0f;
		plotSurface->Title = "Histogram Plot";
		plotSurface->Refresh();
	}
	#pragma endregion
	#pragma region PlotABC
	System::Void CPlotSurface2DDemo::PlotABC()
	{
        array<System::String^>^ lines = gcnew array<System::String^>{
            "ABC (logo for australian broadcasting commission) Example. Demonstrates - ",
            "  * How to set the background of a plotsurface as an image.",
            "  * EqualAspectRatio axis constraint" };

        infoBox->Lines = lines;
        
        plotSurface->Clear();
		const int size = 200;
		array<float>^ xs = gcnew array<float>(size);
		array<float>^ ys = gcnew array<float>(size);
		for (int i=0; i<size; i++)
		{
			xs[i] = (float)Math::Sin((double)i/(double)(size-1)*2.0*Math::PI);
			ys[i] = (float)Math::Cos((double)i/(double)(size-1)*6.0*Math::PI);
		}

		LinePlot^ lp = gcnew LinePlot();
		lp->OrdinateData = ys;
		lp->AbscissaData = xs;
		Pen^ linePen = gcnew Pen( Color::Yellow, 5.0f );
		lp->Pen = linePen;
		plotSurface->Add(lp); 
		plotSurface->Title = "AxisConstraint.EqualScaling in action...";
 
		// Image downloaded from http://squidfingers.com. Thanks! 
		System::Reflection::Assembly^ a = (System::Reflection::Assembly^)System::Reflection::Assembly::GetExecutingAssembly();
		System::IO::Stream^ file = (System::IO::Stream^)
			a->GetManifestResourceStream( "pattern01.jpg" );  
		System::Drawing::Image^ im = (System::Drawing::Image^)Image::FromStream( file );
		plotSurface->PlotBackImage = gcnew System::Drawing::Bitmap( im );
  
		plotSurface->AddAxesConstraint( gcnew AxesConstraint::AspectRatio( 1.0, PlotSurface2D::XAxisPosition::Top, PlotSurface2D::YAxisPosition::Left ) );
		plotSurface->XAxis1->WorldMin = plotSurface->YAxis1->WorldMin;
		plotSurface->XAxis1->WorldMax = plotSurface->YAxis1->WorldMax;
		plotSurface->SmoothingMode = System::Drawing::Drawing2D::SmoothingMode::AntiAlias;

		plotSurface->AddInteraction(gcnew NPlot::Windows::PlotSurface2D::Interactions::MouseWheelZoom());

        // make sure plot surface colors are as we expect - the wave example changes them.
        //plotSurface->PlotBackColor = Color::White; 
		plotSurface->BackColor = System::Drawing::SystemColors::Control;
        plotSurface->XAxis1->Color = Color::Black;
        plotSurface->YAxis1->Color = Color::Black;

        plotSurface->Refresh();
	}
	#pragma endregion
	#pragma region PlotLabelAxis
	System::Void CPlotSurface2DDemo::PlotLabelAxis()
	{
        array<System::String^>^ lines = gcnew array<System::String^>{
            "Internet Usage Example. Demonstrates - ",
            "  * Label Axis with angular text.",
            "  * RectangleBrushes." };

        infoBox->Lines = lines;
        
        plotSurface->Clear();

		Grid^ mygrid = gcnew Grid();
		mygrid->VerticalGridType = Grid::GridType::Coarse;
		Pen^ majorGridPen = gcnew Pen( Color::LightGray );
		array<float>^ pattern = gcnew array<float> { 1.0f, 2.0f };
		majorGridPen->DashPattern = pattern;
		mygrid->MajorGridPen = majorGridPen;
		plotSurface->Add( mygrid );

		array<float>^ xs = gcnew array<float> {20.0f, 31.0f, 27.0f, 38.0f, 24.0f, 3.0f, 2.0f };
		array<float>^ xs2 = gcnew array<float> {7.0f, 10.0f, 42.0f, 9.0f, 2.0f, 79.0f, 70.0f };
		array<float>^ xs3 = gcnew array<float> {1.0f, 20.0f, 20.0f, 25.0f, 10.0f, 30.0f, 30.0f };

		HistogramPlot^ hp = gcnew HistogramPlot();
		hp->DataSource = xs;
		hp->BaseWidth = 0.6f;
		hp->RectangleBrush =
			gcnew RectangleBrushes::HorizontalCenterFade( Color::FromArgb(255,255,200), Color::White );
		hp->Filled = true;
		hp->Label = "Developer Work";
		
        HistogramPlot^ hp2 = gcnew HistogramPlot();
		hp2->DataSource = xs2;
		hp2->Label = "Web Browsing";
		hp2->RectangleBrush = RectangleBrushes::Horizontal::FaintGreenFade;
		hp2->Filled = true;
		hp2->StackedTo( hp );
		
        HistogramPlot^ hp3 = gcnew HistogramPlot();
		hp3->DataSource = xs3;
		hp3->Label = "P2P Downloads";
		hp3->RectangleBrush = RectangleBrushes::Vertical::FaintBlueFade;
		hp3->Filled = true;
		hp3->StackedTo( hp2 );
		
        plotSurface->Add( hp );
		plotSurface->Add( hp2 );
		plotSurface->Add( hp3 );
		
        plotSurface->Legend = gcnew Legend();

		LabelAxis^ la = gcnew LabelAxis( plotSurface->XAxis1 );
		la->AddLabel( "Monday", 0.0f );
		la->AddLabel( "Tuesday", 1.0f );
		la->AddLabel( "Wednesday", 2.0f );
		la->AddLabel( "Thursday", 3.0f );
		la->AddLabel( "Friday", 4.0f );
		la->AddLabel( "Saturday", 5.0f );
		la->AddLabel( "Sunday", 6.0f );
		la->Label = "Days";
		la->TickTextFont = gcnew System::Drawing::Font( "Courier New", 8 );
		la->TicksBetweenText = true;

		plotSurface->XAxis1 = la;
		plotSurface->YAxis1->WorldMin = 0.0;
		plotSurface->YAxis1->Label = "MBytes";
		((LinearAxis^)plotSurface->YAxis1)->NumberOfSmallTicks = 1;

		plotSurface->Title = "Internet useage for user:\n johnc 09/01/03 - 09/07/03";

		plotSurface->XAxis1->TicksLabelAngle = 30.0f;

		plotSurface->PlotBackBrush = RectangleBrushes::Vertical::FaintRedFade;
		plotSurface->Refresh();
	}
	#pragma endregion
	#pragma region PlotParticles
	System::Void CPlotSurface2DDemo::PlotParticles()
	{
        array<System::String^>^ lines = gcnew array<System::String^>{
            "Particles Example. Demonstrates - ",
            "  * How to chart multiple data sets against multiple axes at the same time."};

        infoBox->Lines = lines;
        
        plotSurface->Clear();

		Grid^ mygrid = gcnew Grid();
		mygrid->HorizontalGridType = Grid::GridType::Fine;
		mygrid->VerticalGridType = Grid::GridType::Fine;
		plotSurface->Add( mygrid );

		// in this example we synthetize a particle distribution
		// in the x-x' phase space and plot it, with the rms Twiss
		// ellipse and desnity distribution
		const int Particle_Number = 500;
		array<float>^ x = gcnew array<float>(Particle_Number);
		array<float>^ y = gcnew array<float>(Particle_Number);
		// Twiss parameters for the beam ellipse
		// 5 mm mrad max emittance, 1 mm beta function
		float alpha, beta, gamma, emit;
		alpha = -2.0f;
		beta = 1.0f;
		gamma = (1.0f + alpha * alpha) / beta;
		emit = 4.0f;

		float da, xmax, xpmax;
		da = -alpha / gamma;
		xmax = (float)Math::Sqrt(emit / gamma);
		xpmax = (float)Math::Sqrt(emit * gamma);

		Random^ rand = gcnew Random();

		// cheap randomizer on the unit circle
		for (int i = 0; i<Particle_Number; i++)
		{
			float r;
			do
			{
				x[i] = (float)(2.0f * rand->NextDouble() - 1.0f);
				y[i] = (float)(2.0f * rand->NextDouble() - 1.0f);
				r = (float)Math::Sqrt(x[i] * x[i] + y[i] * y[i]);
			} while (r > 1.0f);
		}

		// transform to the tilted twiss ellipse
		for (int i =0; i<Particle_Number; ++i)
		{
			y[i] *= xpmax;
			x[i] = x[i] * xmax + y[i] * da;
		}
		plotSurface->Title = "Beam Horizontal Phase Space and Twiss ellipse";

		PointPlot^ pp = gcnew PointPlot();
		pp->OrdinateData = y;
		pp->AbscissaData = x;
		pp->Marker = gcnew Marker(Marker::MarkerType::FilledCircle ,4, gcnew Pen(Color::Blue));
		plotSurface->Add(pp, PlotSurface2D::XAxisPosition::Bottom, PlotSurface2D::YAxisPosition::Left);

		// set axes
		LinearAxis^ lx = (LinearAxis^) plotSurface->XAxis1;
		lx->Label = "Position - x [mm]";
		lx->NumberOfSmallTicks = 2;
		LinearAxis^ ly = (LinearAxis^) plotSurface->YAxis1;
		ly->Label = "Divergence - x' [mrad]";
		ly->NumberOfSmallTicks = 2;
		
		// Draws the rms Twiss ellipse computed from the random data
		array<float>^ xeli=gcnew array<float>(40);
		array<float>^ yeli=gcnew array<float>(40);

		float a_rms, b_rms, g_rms, e_rms;

		Twiss(x, y, a_rms, b_rms, g_rms, e_rms);
		TwissEllipse(a_rms, b_rms, g_rms, e_rms, xeli, yeli);

		LinePlot^ lp = gcnew LinePlot();
		lp->OrdinateData = yeli;
		lp->AbscissaData = xeli;
		plotSurface->Add(lp, PlotSurface2D::XAxisPosition::Bottom, PlotSurface2D::YAxisPosition::Left);
		lp->Pen = gcnew Pen( Color::Red, 2.0f );
		// Draws the ellipse containing 100% of the particles
		// for a uniform distribution in 2D the area is 4 times the rms
		array<float>^ xeli2 = gcnew array<float>(40);
		array<float>^ yeli2 = gcnew array<float>(40);
		TwissEllipse(a_rms, b_rms, g_rms, 4.0F * e_rms, xeli2, yeli2);

		LinePlot^ lp2 = gcnew LinePlot();
		lp2->OrdinateData = yeli2;
		lp2->AbscissaData = xeli2;
		plotSurface->Add( lp2, PlotSurface2D::XAxisPosition::Bottom, PlotSurface2D::YAxisPosition::Left );
		Pen^ p2 = gcnew Pen( Color::Red, 2.0f );
		array<float>^ pattern = gcnew array<float>{ 5.0f, 40.0f };
		p2->DashPattern = pattern;
		lp2->Pen = p2;

		// now bin the particle position to create beam density histogram
		float range, min, max;
		min = (float)lx->WorldMin;
		max = (float)lx->WorldMax;
		range = max - min;

		const int Nbin = 30;
		float dx = range / Nbin;
		array<float>^ xbin = gcnew array<float>(Nbin+1);
		array<float>^ xh = gcnew array<float>(Nbin+1);

		for (int j=0; j<=Nbin; ++j)
		{
			xbin[j] = min + j * range;
			if (j < Nbin) xh[j] = 0.0F;
		}
		for (int i =0; i<Particle_Number; ++i)
		{
			if (x[i] >= min && x[i] <= max)
			{
				int j;
				j = Convert::ToInt32(Nbin * (x[i] - min) / range);
				xh[j] += 1;
			}
		}
		StepPlot^ sp= gcnew StepPlot();
		sp->OrdinateData = xh;
		sp->AbscissaData = gcnew StartStep( min, range / Nbin );
		sp->Center = true;
		plotSurface->Add(sp, PlotSurface2D::XAxisPosition::Bottom, PlotSurface2D::YAxisPosition::Right);
		// axis formatting
		LinearAxis^ ly2 = (LinearAxis^)plotSurface->YAxis2;
		ly2->WorldMin = 0.0f;
		ly2->Label = "Beam Density [a.u.]";
		ly2->NumberOfSmallTicks = 2;
		sp->Pen = gcnew Pen( Color::Green, 2 );

		// Finally, refreshes the plot
		plotSurface->Refresh();
	}

	// Fill the array containing the rms twiss ellipse data points
	// ellipse is g*x^2+a*x*y+b*y^2=e
	System::Void CPlotSurface2DDemo::TwissEllipse(float a, float b, float g, float e, array<float>^ x, array<float>^ y)
	{
		float rot, sr, cr, brot;
		if (a==0) 
		{
			rot=0;
		}
		else
		{
			rot=(float)(.5*Math::Atan(2.0 * a / (g - b)));
		}
		sr = (float)Math::Sin(rot);
		cr = (float)Math::Cos(rot);
		brot = g * sr * sr - 2.0F * a * sr * cr + b * cr * cr;
		int npt=x->Length;
		float theta;
	
		for (int i=0; i<npt;++i)
		{
			float xr,yr;
			theta = i * 2.0F * (float)Math::PI / (npt-1);
			xr = (float)(Math::Sqrt(e * brot) * Math::Cos(theta));
			yr = (float)(Math::Sqrt(e / brot) * Math::Sin(theta));
			x[i] = xr * cr - yr * sr;
			y[i] = xr * sr + yr * cr;
		}
	}
	// Evaluates the rms Twiss parameters from the particle coordinates
	System::Void CPlotSurface2DDemo::Twiss(array<float>^ x, array<float>^ y, float& a, float& b, float& g, float& e)
	{
		float xave, xsqave, yave, ysqave, xyave;
		float sigmaxsq, sigmaysq, sigmaxy;
		int Npoints= x->Length;
		xave = 0;
		yave = 0;
		for (int i=0; i<Npoints; ++i)
		{
			xave += x[i];
			yave += y[i];
		}
		xave /= Npoints;
		yave /= Npoints;
		xsqave = 0;
		ysqave = 0;
		xyave = 0;
		for (int i=0;i<Npoints;i++)
		{
			xsqave += x[i] * x[i];
			ysqave += y[i] * y[i];
			xyave += x[i] * y[i];
		}
		xsqave /= Npoints;
		ysqave /= Npoints;
		xyave /= Npoints;
		sigmaxsq = xsqave - xave * xave;
		sigmaysq = ysqave - yave * yave;
		sigmaxy = xyave - xave * yave;
		// Now evaluates rms Twiss parameters
		e = (float)Math::Sqrt(sigmaxsq * sigmaysq - sigmaxy * sigmaxy);
		a = -sigmaxy / e;
		b = sigmaxsq / e;
		g = (1.0F + a * a) / b;
	}
	#pragma endregion
	#pragma region PlotQE
	System::Void CPlotSurface2DDemo::PlotQE()
	{
        array<System::String^>^ lines = gcnew array<System::String^>{
            "Cs2Te Photocathode QE evolution Example. Demonstrates - ",
            "  * LabelPointPlot (allows text to be associated with points)",
            "  * PointPlot droplines",
            "  * LabelAxis",
		    "  * PhysicalSpacingMin property of LabelAxis",
            "",
            "You cannot interact with this chart"};
        infoBox->Lines = lines;
        
        qeExampleTimer->Enabled = true;
		plotSurface->Clear();
		
		int len = 24;
		array<System::String^>^ s = gcnew array<System::String^>(len);
		PlotQEExampleValues = gcnew array<double>(len);
		PlotQEExampleTextValues = gcnew array<System::String^>(len);

		Random^ r = gcnew Random();

		for (int i=0; i<len;i++)
		{
			PlotQEExampleValues[i] = 8.0f + 12.0f * (double)r->Next(10000) / 10000.0f;
			if (PlotQEExampleValues[i] > 18.0f)
			{
				PlotQEExampleTextValues[i] = "KCsTe";
			}
			else
			{
				PlotQEExampleTextValues[i] = "";
			}
			s[i] = i.ToString("00") + ".1";
		}

		PointPlot^ pp = gcnew PointPlot();
		pp->DataSource = PlotQEExampleValues;
		pp->Marker = gcnew Marker( Marker::MarkerType::Square, 10 );
		pp->Marker->DropLine = true;
		pp->Marker->Pen = Pens::CornflowerBlue;
		pp->Marker->Filled = false;
		plotSurface->Add( pp );

		LabelPointPlot^ tp1 = gcnew LabelPointPlot();
		tp1->DataSource = PlotQEExampleValues;
		tp1->TextData = PlotQEExampleTextValues;
		tp1->LabelTextPosition = LabelPointPlot::LabelPositions::Above;
		tp1->Marker = gcnew Marker( Marker::MarkerType::None, 10 );
		plotSurface->Add( tp1 );

		LabelAxis^ la = gcnew LabelAxis( plotSurface->XAxis1 );
		for (int i=0; i<len; ++i)
		{
			la->AddLabel( s[i], i );
		}
		FontFamily^ ff = gcnew System::Drawing::FontFamily( "Verdana" );
		la->TickTextFont = gcnew System::Drawing::Font( ff, 7 );
		la->PhysicalSpacingMin = 25;
		plotSurface->XAxis1 = la;

		plotSurface->Title = "Cs2Te Photocathode QE evolution";
		plotSurface->TitleFont = gcnew System::Drawing::Font(ff,15);
		plotSurface->XAxis1->WorldMin = -1.0f;
		plotSurface->XAxis1->WorldMax = len;
		plotSurface->XAxis1->LabelFont = gcnew System::Drawing::Font( ff, 10 );
		plotSurface->XAxis1->Label = "Cathode ID";
		plotSurface->YAxis1->Label = "QE [%]";
		plotSurface->YAxis1->LabelFont = gcnew System::Drawing::Font( ff, 10 );
		plotSurface->YAxis1->TickTextFont = gcnew System::Drawing::Font( ff, 10 );

		plotSurface->YAxis1->WorldMin = 0.0;
		plotSurface->YAxis1->WorldMax= 25.0;
 
		plotSurface->XAxis1->TicksLabelAngle = 60.0f;

		plotSurface->Refresh();
	}
	#pragma endregion
	#pragma region PlotDataSet
	System::Void CPlotSurface2DDemo::PlotDataSet()
	{
        array<System::String^>^ lines = gcnew array<System::String^>{
            "Stock Data Example. Demonstrates - ",
            "  * CandlePlot, FilledRegion, LinePlot and ArrowItem IDrawables",
            "  * DateTime axes",
            "  * A few plot interactions. Try (a) dragging the axes (b) dragging the plot surface."};
        infoBox->Lines = lines;
        
        plotSurface->Clear();
		//plotSurface->DateTimeToolTip = true;

		// obtain stock information from xml file
		DataSet^ ds = gcnew DataSet();
		System::IO::Stream^ file = (System::IO::Stream^)
			System::Reflection::Assembly::GetExecutingAssembly()->GetManifestResourceStream( "asx_jbh.xml" );
		ds->ReadXml( file, System::Data::XmlReadMode::ReadSchema );
		DataTable^ dt = ds->Tables[0];
		DataView^ dv = gcnew DataView( dt );

		// create CandlePlot.
		CandlePlot^ cp = gcnew CandlePlot();
		cp->DataSource = dt;
		cp->AbscissaData = "Date";
		cp->OpenData = "Open";
		cp->LowData = "Low";
		cp->HighData = "High";
		cp->CloseData = "Close";
		cp->BearishColor = Color::Red;
		cp->BullishColor = Color::Green;
		cp->Style = CandlePlot::Styles::Filled;

		// calculate 10 day moving average and 2*sd line
		ArrayList^ av10 = gcnew ArrayList();
		ArrayList^ sd2_10 = gcnew ArrayList();
		ArrayList^ sd_2_10 = gcnew ArrayList();
		ArrayList^ dates = gcnew ArrayList();
		for (int i=0; i<dt->Rows->Count-10; ++i)
		{
			float sum = 0.0f;
			for (int j=0; j<10; ++j)
			{
				sum += (float)dt->Rows[i+j]["Close"];
			}
			float average = sum / 10.0f;
			av10->Add( average );
			sum = 0.0f;
			for (int j=0; j<10; ++j)
			{
				sum += ((float)dt->Rows[i+j]["Close"]-average)*((float)dt->Rows[i+j]["Close"]-average);
			}
			sum /= 10.0f;
			sum = 2.0f*(float)Math::Sqrt( sum );
			sd2_10->Add( average + sum );
			sd_2_10->Add( average - sum );
			dates->Add( (DateTime)dt->Rows[i+10]["Date"] );
		}

		// and a line plot of close values.
		LinePlot^ av = gcnew LinePlot();
		av->OrdinateData = av10;
		av->AbscissaData = dates;
		av->Color = Color::LightGray;
		av->Pen->Width = 2.0f;

		LinePlot^ top = gcnew LinePlot();
		top->OrdinateData = sd2_10;
		top->AbscissaData = dates;
		top->Color = Color::LightSteelBlue;
		top->Pen->Width = 2.0f;

		LinePlot^ bottom = gcnew LinePlot();
		bottom->OrdinateData = sd_2_10;
		bottom->AbscissaData = dates;
		bottom->Color = Color::LightSteelBlue;
		bottom->Pen->Width = 2.0f;

		FilledRegion^ fr = gcnew FilledRegion( top, bottom );
		//fr.RectangleBrush = gcnew RectangleBrushes.Vertical( Color::FloralWhite, Color::GhostWhite );
		fr->RectangleBrush = gcnew RectangleBrushes::Vertical( Color::FromArgb(255,255,240), Color::FromArgb(240,255,255) );
		plotSurface->SmoothingMode = System::Drawing::Drawing2D::SmoothingMode::AntiAlias;

		plotSurface->Add( fr );

		plotSurface->Add( gcnew Grid() );

		plotSurface->Add( av );
		plotSurface->Add( top );
		plotSurface->Add( bottom );
		plotSurface->Add( cp );

		// now make an arrow... 
		ArrowItem^ arrow = gcnew ArrowItem( (PointD)gcnew PointD( (double)(((DateTime^)dt->Rows[60]["Date"])->Ticks), 2.28 ), -80, "An interesting flat bit" );
		arrow->ArrowColor = Color::DarkBlue;
		arrow->PhysicalLength = 50;
		
		//plotSurface->Add( arrow );

		plotSurface->Title = "AU:JBH";
		plotSurface->XAxis1->Label = "Date / Time";
        plotSurface->XAxis1->WorldMin += plotSurface->XAxis1->WorldLength / 4.0;
        plotSurface->XAxis1->WorldMax -= plotSurface->XAxis1->WorldLength / 2.0;
        plotSurface->YAxis1->Label = "Price [$]";

		plotSurface->XAxis1 = gcnew TradingDateTimeAxis( plotSurface->XAxis1 );

		plotSurface->AddInteraction(gcnew NPlot::Windows::PlotSurface2D::Interactions::HorizontalDrag());
		plotSurface->AddInteraction(gcnew NPlot::Windows::PlotSurface2D::Interactions::VerticalDrag());
		plotSurface->AddInteraction(gcnew NPlot::Windows::PlotSurface2D::Interactions::AxisDrag(true));


        // make sure plot surface colors are as we expect - the wave example changes them.
        plotSurface->PlotBackColor = Color::White;
		plotSurface->BackColor = System::Drawing::SystemColors::Control;
        plotSurface->XAxis1->Color = Color::Black;
        plotSurface->YAxis1->Color = Color::Black;

        plotSurface->Refresh();
	}
	#pragma endregion
	#pragma region PlotImage
	System::Void CPlotSurface2DDemo::PlotImage()
	{

        array<System::String^>^ lines = gcnew array<System::String^>{
            "Image Example. Demonstrates - ",
            "  * ImagePlot IDrawable",
            "  * Rubber band selection plot interaction" };
        infoBox->Lines = lines;
        
		System::String^ myfile = 
"-1.251382E-3 -1.279191E-3 -7.230207E-4 -8.064462E-4 -5.005528E-4 -5.839783E-4 -1.696318E-3 -1.668509E-3 -3.893189E-4 -4.449358E-4 -1.473850E-3 -1.473850E-3 -1.974403E-3 -1.946594E-3 -2.085637E-3 -2.085637E-3 -1.612892E-3 -1.640701E-3 -1.863169E-3 " +
"-1.251382E-3 -1.306999E-3 -6.674037E-4 -8.620631E-4 -4.449358E-4 -6.674037E-4 -1.668509E-4 -1.668509E-3 -3.615103E-4 -5.005528E-4 -5.561698E-5 -1.473850E-3 -4.449358E-4 -1.946594E-3 -6.395953E-4 -2.057828E-3 -1.585084E-3 -1.696318E-3 -1.807552E-3 " +
"-1.223573E-3 -1.306999E-3 -6.117867E-4 -9.176802E-4 -3.893189E-4 -7.508292E-4 -1.390424E-4 -1.640701E-3 -3.615103E-4 -5.561698E-4 -5.561698E-5 -1.446041E-3 -4.449358E-4 -1.918786E-3 -6.117867E-4 -2.057828E-3 -1.585084E-3 -1.724126E-3 -1.779743E-3 " +
"-1.251382E-3 -1.334807E-3 -5.839783E-4 -9.732971E-4 -3.615103E-4 -8.342547E-4 -1.390424E-4 3.893189E-4 1.751935E-3 2.919891E-3 3.476061E-3 3.031125E-3 1.807552E-3 6.674037E-4 -6.117867E-4 -2.030020E-3 -1.585084E-3 -1.779743E-3 -1.779743E-3 " +
"-1.279191E-3 -1.362616E-3 -5.561698E-4 -1.028914E-3 -3.615103E-4 8.620631E-4 2.335913E-3 3.114551E-3 4.087848E-3 5.227996E-3 6.395952E-3 5.700740E-3 4.560592E-3 2.502764E-3 1.362616E-3 -6.117867E-4 -1.585084E-3 -1.807552E-3 -1.751935E-3 " +
"-1.306999E-3 -1.390424E-3 -5.561698E-4 -1.056723E-3 1.890977E-3 4.087848E-3 6.117868E-3 9.621738E-3 1.357054E-2 1.721345E-2 1.715784E-2 1.462726E-2 1.059503E-2 6.368144E-3 3.253593E-3 1.279191E-3 6.674037E-4 -1.807552E-3 -1.779743E-3 " +
"-1.390424E-3 -1.390424E-3 -5.561698E-4 1.585084E-3 4.560592E-3 8.481589E-3 1.437699E-2 2.155158E-2 2.702985E-2 3.078400E-2 3.134017E-2 2.892083E-2 2.338694E-2 1.446041E-2 6.757463E-3 3.031125E-3 6.674037E-4 -1.807552E-3 -1.807552E-3 " +
"-1.446041E-3 -1.362616E-3 1.140148E-3 3.448253E-3 7.647335E-3 1.512782E-2 2.360941E-2 3.125674E-2 3.520555E-2 3.673501E-2 3.692967E-2 3.598418E-2 3.345361E-2 2.466613E-2 1.415452E-2 5.700740E-3 3.114551E-3 8.342547E-5 -1.835360E-3 " +
"-1.529467E-3 -1.334807E-3 1.112340E-3 5.367038E-3 1.154052E-2 2.080075E-2 3.011659E-2 3.581733E-2 3.751365E-2 3.676282E-2 3.687406E-2 3.776393E-2 3.598418E-2 3.139579E-2 1.999430E-2 9.315844E-3 3.142359E-3 1.112340E-4 -1.863169E-3 " +
"-1.640701E-3 -1.306999E-3 1.084531E-3 6.785271E-3 1.557275E-2 2.410996E-2 3.311991E-2 3.584514E-2 3.748584E-2 3.681844E-2 3.681844E-2 3.776393E-2 3.592857E-2 3.350923E-2 2.177405E-2 1.140148E-2 3.114551E-3 1.112340E-4 -1.890977E-3 " +
"-1.696318E-3 -1.251382E-3 1.056723E-3 6.813080E-3 1.557275E-2 2.413777E-2 3.311991E-2 3.756927E-2 3.745804E-2 3.687406E-2 3.676282E-2 3.776393E-2 3.590076E-2 3.350923E-2 2.174624E-2 1.142929E-2 3.114551E-3 1.390424E-4 -1.918786E-3 " +
"-1.779743E-3 -1.195765E-3 1.028914E-3 6.785271E-3 1.256944E-2 2.180186E-2 3.039468E-2 3.598418E-2 3.743023E-2 3.695748E-2 3.673501E-2 3.773612E-2 3.587295E-2 2.967166E-2 1.874292E-2 9.593928E-3 3.086742E-3 1.668509E-4 -1.918786E-3 " +
"-1.863169E-3 -1.140148E-3 -1.362616E-3 3.058934E-3 7.285824E-3 1.532248E-2 2.472175E-2 3.195195E-2 3.478842E-2 3.701310E-2 3.670720E-2 3.545582E-2 3.220223E-2 2.333132E-2 1.354273E-2 5.978825E-3 1.279191E-3 1.668509E-4 -1.918786E-3 " +
"-1.918786E-3 -1.084531E-3 -1.390424E-3 6.674037E-4 3.448253E-3 7.563909E-3 1.596207E-2 2.394311E-2 2.875398E-2 3.103427E-2 3.181291E-2 2.972727E-2 2.363721E-2 1.543371E-2 9.176801E-3 5.978825E-3 1.251382E-3 1.668509E-4 -1.890977E-3 " +
"-1.974403E-3 -1.056723E-3 -1.390424E-3 6.674037E-4 1.362616E-3 3.781955E-3 8.036654E-3 1.304218E-2 1.824237E-2 2.127349E-2 2.174624E-2 1.963279E-2 1.573960E-2 1.148491E-2 7.341441E-3 3.197976E-3 -1.446041E-3 -1.557275E-3 -1.835360E-3 " +
"-2.030020E-3 -1.056723E-3 -1.334807E-3 -1.084531E-3 -1.279191E-3 1.001106E-3 3.948805E-3 6.674037E-3 9.983247E-3 1.243039E-2 1.426576E-2 1.454384E-2 1.184642E-2 8.787482E-3 4.504975E-3 6.952122E-4 -1.418233E-3 -1.557275E-3 -1.779743E-3 " +
"-2.057828E-3 -1.056723E-3 -1.306999E-3 -1.084531E-3 -1.251382E-3 -7.786377E-4 6.395953E-4 2.586189E-3 4.254699E-3 6.117868E-3 7.619526E-3 7.508292E-3 5.951017E-3 3.003317E-3 -9.176802E-4 -1.195765E-3 -1.362616E-3 -1.557275E-3 -1.751935E-3 " +
"-2.085637E-3 -1.084531E-3 -1.251382E-3 -1.112340E-3 -1.223573E-3 -7.786377E-4 -8.620631E-4 -3.893189E-4 -4.449358E-4 3.337019E-4 1.167957E-3 9.454886E-4 3.893189E-4 -8.342547E-4 -9.176802E-4 -1.195765E-3 -1.306999E-3 -1.585084E-3 -1.696318E-3 " +
"-2.113445E-3 -1.140148E-3 -1.195765E-3 -1.140148E-3 -1.167957E-3 -7.786377E-4 -8.342547E-4 -3.893189E-4 -4.171273E-4 -1.279191E-3 -1.251382E-3 -1.195765E-3 -1.195765E-3 -8.342547E-4 -8.620631E-4 -1.223573E-3 -1.251382E-3 -1.612892E-3 -1.668509E-3";
		
		//string [] tokens = myfile.Split(gcnew char [1] {' '});
		array<System::String^>^ tokens = (array<System::String^>^)myfile->Split(gcnew array<wchar_t>{' '});
		array<double,2>^ map = gcnew array<double,2>(19,19);
		
		for (int i=0; i < 19; ++i)
		{
			for (int j=0; j < 19; ++j)
			{
				map[i,j] = Convert::ToDouble(tokens[i*19+j], gcnew
					System::Globalization::CultureInfo("en-US"));
			}
		}

		plotSurface->Clear();
        
        plotSurface->Title = "Cathode 11.2 QE Map";
		
		ImagePlot^ ip = gcnew ImagePlot(map, -9.0f, 1.0f, -9.0f, 1.0f);
		//ip.Gradient = gcnew StepGradient( StepGradient.Type.Rainbow );
		ip->Gradient = gcnew LinearGradient( Color::Gold, Color::Black );

		plotSurface->Add(ip);
		plotSurface->XAxis1->Label = "x [mm]";
		plotSurface->YAxis1->Label = "y [mm]";

		plotSurface->SmoothingMode = System::Drawing::Drawing2D::SmoothingMode::None;

		//plotSurface->AddAxesConstraint( gcnew AxesConstraint.AxisPosition( PlotSurface2D.YAxisPosition.Left, 0) );
		//plotSurface->AddAxesConstraint( gcnew AxesConstraint.AxisPosition( PlotSurface2D.XAxisPosition.Top, 0.0f) );
		//plotSurface->AddAxesConstraint(
		//	gcnew AxesConstraint.YPixelWorldLength(0.1f,PlotSurface2D.XAxisPosition.Bottom) );
		//plotSurface->AddAxesConstraint( gcnew AxesConstraint.AspectRatio(1.0,PlotSurface2D.XAxisPosition.Top,PlotSurface2D.YAxisPosition.Left) );

		plotSurface->AddInteraction(gcnew NPlot::Windows::PlotSurface2D::Interactions::RubberBandSelection());

        plotSurface->Refresh();

	}
	#pragma endregion
	#pragma region PlotMarkers
	System::Void CPlotSurface2DDemo::PlotMarkers()
	{
        array<System::String^>^ lines = gcnew array<System::String^>{
            "Markers Example. Demonstrates - ",
            "  * PointPlot and the available marker types",
            "  * Legends, and how to place them." };
        infoBox->Lines = lines;
        
        plotSurface->Clear();
		
		array<double>^ y = gcnew array<double> {1.0f};
		for each (System::Object^ i in Enum::GetValues(Marker::MarkerType::typeid))
		{
			Marker^ m = gcnew Marker( (Marker::MarkerType)Enum::Parse(Marker::MarkerType::typeid, i->ToString()), 8 );
			array<double>^ x = gcnew array<double>(1);
			x[0] = (double) m->Type;
			PointPlot^ pp = gcnew PointPlot();
			pp->OrdinateData = y;
			pp->AbscissaData = x;
			pp->Marker = m;
			pp->Label = m->Type.ToString();
			plotSurface->Add( pp );
		}
		plotSurface->Title = "Markers";
		plotSurface->YAxis1->Label = "Index";
		plotSurface->XAxis1->Label = "Marker";
		plotSurface->YAxis1->WorldMin = 0.0f;
		plotSurface->YAxis1->WorldMax = 2.0f;
		plotSurface->XAxis1->WorldMin -= 1.0f;
		plotSurface->XAxis1->WorldMax += 1.0f;

		Legend^ legend = gcnew Legend();
		legend->AttachTo( PlotSurface2D::XAxisPosition::Top, PlotSurface2D::YAxisPosition::Right );
		legend->VerticalEdgePlacement = Legend::Placement::Outside;
		legend->HorizontalEdgePlacement = Legend::Placement::Inside;
        legend->XOffset = 5; // note that these numbers can be negative.
        legend->YOffset = 0;
        plotSurface->Legend = legend;

		plotSurface->Refresh();
	}
	#pragma endregion
	#pragma region PlotCandle
	System::Void CPlotSurface2DDemo::PlotCandle()
	{
		plotSurface->Clear();

		// obtain stock information from xml file
		DataSet^ ds = gcnew DataSet();
		System::IO::Stream^ file = (System::IO::Stream^)
			System::Reflection::Assembly::GetExecutingAssembly()->GetManifestResourceStream( "NPlotDemo.resources.asx_jbh.xml" );
		ds->ReadXml( file, System::Data::XmlReadMode::ReadSchema );
		DataTable^ dt = (DataTable^)ds->Tables[0];

		// create CandlePlot.
		CandlePlot^ cp = gcnew CandlePlot();
		cp->DataSource = dt;
		cp->AbscissaData = "Date";
		cp->OpenData = "Open";
		cp->LowData = "Low";
		cp->HighData = "High";
		cp->CloseData = "Close";
		cp->BearishColor = Color::Red;
		cp->BullishColor = Color::Green;
		cp->StickWidth = 3;
		cp->Color = Color::DarkBlue;

		plotSurface->Add( gcnew Grid() );
		plotSurface->Add( cp );

		plotSurface->Title = "AU:JBH";
		plotSurface->XAxis1->Label = "Date / Time";
		plotSurface->YAxis1->Label = "Price [$]";

		plotSurface->Refresh();
	}
	#pragma endregion
	#pragma region PlotTest
	System::Void CPlotSurface2DDemo::PlotTest()
	{
		plotSurface->Clear();

		// can plot different types.
		ArrayList^ l = gcnew ArrayList();
		l->Add( (int)2 );
		l->Add( (double)1.0 );
		l->Add( (float)3.0 );
		l->Add( (int)5.0 );

		LinePlot^ lp1 = gcnew LinePlot( gcnew array<double> {4.0, 3.0, 5.0, 8.0} );
		lp1->Pen = gcnew Pen( Color::LightBlue );
		lp1->Pen->Width = 2.0f;

		//lp.AbscissaData = gcnew StartStep( 0.0, 2.0 );

		LinePlot^ lp2 = gcnew LinePlot( gcnew array<double> {2.0, 1.0, 4.0, 5.0} );
		lp2->Pen = gcnew Pen( Color::LightBlue );
		lp2->Pen->Width = 2.0f;

		FilledRegion^ fr = gcnew FilledRegion( lp1, lp2 );

		plotSurface->Add(fr);

		plotSurface->Add( gcnew Grid() );
		plotSurface->Add(lp1);
		plotSurface->Add(lp2);

		ArrowItem^ a = gcnew ArrowItem( (PointD)gcnew PointD( 2, 4 ), -50.0f, "Arrow" );
		a->HeadOffset = 5;
		a->ArrowColor = Color::Red;
		a->TextColor = Color::Purple;
		plotSurface->Add( a );

		MarkerItem^ m = gcnew MarkerItem( gcnew Marker( Marker::MarkerType::TriangleDown, 8, Color::ForestGreen ), 1.38, 2.9 );
		plotSurface->Add( m );

		plotSurface->XAxis1->TicksCrossAxis = true;
		
		((LinearAxis^)plotSurface->XAxis1)->LargeTickValue = -4.1;
		((LinearAxis^)plotSurface->XAxis1)->AutoScaleText = true;
		((LinearAxis^)plotSurface->XAxis1)->TicksIndependentOfPhysicalExtent = true;
		

		plotSurface->Refresh();
	}
	#pragma endregion
	#pragma region PlotWave

    System::Void CPlotSurface2DDemo::PlotWave()
	{
		array<System::String^>^ lines = gcnew array<System::String^> {
            "Sound Wave Example. Demonstrates - ",
            "  * StepPlot (centered) and HorizontalLine IDrawables",
            "  * How to set colors of various things.",
            "  * A few plot interactions. Try left clicking and dragging (a) the axes (b) in the plot region.",
            "  * In the future I plan add a plot interaction for axis drag that knows if the ctr key is down. This will select between drag/scale" };
        infoBox->Lines = lines;

        //FileStream file = gcnew FileStream( @"c:\light.wav", System.IO.FileMode.Open ); 
		System::IO::Stream^ file = (System::IO::Stream^)
			System::Reflection::Assembly::GetExecutingAssembly()->GetManifestResourceStream("light.wav");

        
		array<System::Int16>^ w = gcnew array<System::Int16>(5000); 
		array<Byte>^ a = gcnew array<Byte>(10000);
		file->Read( a, 0, 10000 );
		for (int i=100; i<5000; ++i)
		{
			w[i] = BitConverter::ToInt16(a,i*2);
		} 

        file->Close(); 

        plotSurface->Clear();
		plotSurface->AddInteraction(gcnew NPlot::Windows::PlotSurface2D::Interactions::VerticalGuideline());
		plotSurface->AddInteraction(gcnew NPlot::Windows::PlotSurface2D::Interactions::HorizontalRangeSelection(3));
		plotSurface->AddInteraction(gcnew NPlot::Windows::PlotSurface2D::Interactions::AxisDrag(true));

        plotSurface->Add(gcnew HorizontalLine(0.0, Color::LightBlue));
        
        StepPlot^ sp = gcnew StepPlot();
		sp->DataSource = w;
        sp->Color = Color::Yellow;
        sp->Center = true;
        plotSurface->Add( sp );

        plotSurface->YAxis1->FlipTicksLabel = true;

        plotSurface->PlotBackColor = Color::DarkBlue;
        plotSurface->BackColor = Color::Black;
        plotSurface->XAxis1->Color = Color::White;
        plotSurface->YAxis1->Color = Color::White;
        
        plotSurface->Refresh();
    
	}
	#pragma endregion
    #pragma region PlotMultiHistogram
    System::Void CPlotSurface2DDemo::PlotMultiHistogram()
    {
        array<double>^ data = gcnew array<double>  { 0, 4, 3, 2, 5, 4, 2, 3 };
        array<double>^ data2 = gcnew array<double> { 5, 2, 4, 1, 2, 1, 5, 3 };

        HistogramPlot^ hp = gcnew HistogramPlot();
        hp->OrdinateData = data;
		hp->RectangleBrush = RectangleBrushes::Horizontal::FaintRedFade;
        hp->Filled = true;
        hp->BaseOffset = -0.15;
        hp->BaseWidth = 0.25f;

        HistogramPlot^ hp2 = gcnew HistogramPlot();
        hp2->OrdinateData = data2;
		hp2->RectangleBrush = RectangleBrushes::Horizontal::FaintGreenFade;
        hp2->Filled = true;
        hp2->BaseOffset = 0.15;
        hp2->BaseWidth = 0.25f;

        plotSurface->Clear();

        plotSurface->Add(hp);
        plotSurface->Add(hp2);

		plotSurface->PlotBackBrush = RectangleBrushes::Vertical::FaintBlueFade;
        plotSurface->Refresh();
    }
    #pragma endregion
    #pragma region PlotCandleSimple
    System::Void CPlotSurface2DDemo::PlotCandleSimple()
    {
        array<System::String^>^ lines = gcnew array<System::String^>{
            "Simple CandlePlot example. Demonstrates - ",
            "  * Setting candle plot datapoints using arrays." };
        infoBox->Lines = lines;

        plotSurface->Clear();

        FilledRegion^ fr = gcnew FilledRegion(
            gcnew VerticalLine(1.2),
            gcnew VerticalLine(2.4));
		fr->Brush = Brushes::BlanchedAlmond;
        plotSurface->Add(fr);

        // note that arrays can be of any type you like.
        array<int>^ opens =  gcnew array<int>{ 1, 2, 1, 2, 1, 3 };
        array<double>^ closes = gcnew array<double>{ 2, 2, 2, 1, 2, 1 };
        array<float>^ lows =   gcnew array<float>{ 0, 1, 1, 1, 0, 0 };
		array<System::Int64>^ highs =  gcnew array<System::Int64>{ 3, 2, 3, 3, 3, 4 };
        array<int>^ times =  gcnew array<int>{ 0, 1, 2, 3, 4, 5 };

        CandlePlot^ cp = gcnew CandlePlot();
        cp->CloseData = closes;
        cp->OpenData = opens;
        cp->LowData = lows;
        cp->HighData = highs;
        cp->AbscissaData = times;
        plotSurface->Add(cp);

		HorizontalLine^ line = gcnew HorizontalLine( 1.2 );
		line->LengthScale = 0.89f;
		plotSurface->Add( line, -10 );

		VerticalLine^ line2 = gcnew VerticalLine( 1.2 );
		line2->LengthScale = 0.89f;
		plotSurface->Add( line2 );

		plotSurface->AddInteraction( gcnew NPlot::Windows::PlotSurface2D::Interactions::MouseWheelZoom() );

		plotSurface->Title = "Line in the Title Number 1\nFollowed by another title line\n and another";
        plotSurface->Refresh();
    }
    #pragma endregion
	#pragma region Mockup
	System::Void CPlotSurface2DDemo::PlotMockup()
	{
		array<System::String^>^ lines = gcnew array<System::String^>{
							 "THE TEST (can your charting library handle this?) - ",
							 "NPlot demonstrates it can handle real world charting requirements." };
		infoBox->Lines = lines;

		// first of all, generate some mockup data.
		DataTable^ info = gcnew DataTable( "Store Information" );
		info->Columns->Add( "Index", Int32::typeid );
		info->Columns->Add( "IndexOffsetLeft", float::typeid );
		info->Columns->Add( "IndexOffsetRight", float::typeid );
		info->Columns->Add( "StoreName", String::typeid );
		info->Columns->Add( "BarBase", float::typeid );
		info->Columns->Add( "StoreGrowth", float::typeid );
		info->Columns->Add( "AverageGrowth", float::typeid );
		info->Columns->Add( "ProjectedSales", float::typeid );

		float barBase = 185.0f;
		Random^ r = gcnew Random();
		for (int i=0; i<18; ++i)
		{
			DataRow^ row = (DataRow^)info->NewRow();
			row["Index"] = i;
			row["IndexOffsetLeft"] = (float)i - 0.1f;
			row["IndexOffsetRight"] = (float)i + 0.1f;
			row["StoreName"] = "Store " + (i+1).ToString();
			row["BarBase"] = barBase;
			row["StoreGrowth"] = barBase + ( (r->NextDouble() - 0.1) * 20.0f );
			row["AverageGrowth"] = barBase + ( (r->NextDouble() - 0.1) * 15.0f );
			row["ProjectedSales"] = barBase + ( r->NextDouble() * 15.0f );
			info->Rows->Add( row );
			barBase += (float)r->NextDouble() * 4.0f;
		}

		
		plotSurface->Clear();

		plotSurface->SmoothingMode = System::Drawing::Drawing2D::SmoothingMode::AntiAlias;

		// generate the grid 
		Grid^ grid = gcnew Grid();
		grid->VerticalGridType = Grid::GridType::Coarse;
		grid->HorizontalGridType = Grid::GridType::None;
		grid->MajorGridPen = gcnew Pen( Color::Black, 1.0f );
		plotSurface->Add( grid );

		// generate the trendline 
		LinePlot^ trendline = gcnew LinePlot();
		trendline->DataSource = info;
		trendline->AbscissaData = "Index";
		trendline->OrdinateData = "BarBase";
		trendline->Pen = gcnew Pen( Color::Black, 3.0f );
		trendline->Label = "Trendline";
		plotSurface->Add( trendline );

		// draw store growth bars
		BarPlot^ storeGrowth = gcnew BarPlot();
		storeGrowth->DataSource = info;
		storeGrowth->AbscissaData = "IndexOffsetLeft";
		storeGrowth->OrdinateDataTop = "StoreGrowth";
		storeGrowth->OrdinateDataBottom = "BarBase";
		storeGrowth->Label = "Store Growth";
		storeGrowth->FillBrush = NPlot::RectangleBrushes::Solid::Black;
		//storeGrowth.BorderPen = gcnew Pen( Color::Black, 2.0f );
		plotSurface->Add( storeGrowth );

		// draw average growth bars
		BarPlot^ averageGrowth = gcnew BarPlot();
		averageGrowth->DataSource = info;
		averageGrowth->AbscissaData = "IndexOffsetRight";
		averageGrowth->OrdinateDataBottom = "BarBase";
		averageGrowth->OrdinateDataTop = "AverageGrowth";
		averageGrowth->Label = "Average Growth";
		averageGrowth->FillBrush = NPlot::RectangleBrushes::Solid::Gray;
		//averageGrowth.BorderPen = gcnew Pen( Color::Black, 2.0f );
		plotSurface->Add( averageGrowth );

		// generate the projected sales step line.
		StepPlot^ projected = gcnew StepPlot();
		projected->DataSource = info;
		projected->AbscissaData = "Index";
		projected->OrdinateData = "ProjectedSales";
		projected->Pen = gcnew Pen( Color::Orange, 3.0f );
		projected->HideVerticalSegments = true;
		projected->Center = true;
		projected->Label = "Projected Sales";
		projected->WidthScale = 0.7f;
		plotSurface->Add( projected );

		// generate the minimum target line.
		HorizontalLine^ minimumTargetLine = gcnew HorizontalLine( 218, gcnew Pen( Color::Green, 3.5f ) );
		minimumTargetLine->Label = "Minimum Target";
		minimumTargetLine->LengthScale = 0.98f;
		minimumTargetLine->ShowInLegend = true; // off by default for lines.
		plotSurface->Add( minimumTargetLine );

		// generate the preferred target line.
		HorizontalLine^ preferredTargetLine = gcnew HorizontalLine( 228, gcnew Pen( Color::Blue, 3.5f ) );
		preferredTargetLine->Label = "Preferred Target";
		preferredTargetLine->LengthScale = 0.98f;
		preferredTargetLine->ShowInLegend = true; // off by default for lines.
		plotSurface->Add( preferredTargetLine );

		// make some modifications so that chart matches requirements.
		// y axis.
		plotSurface->YAxis1->TicksIndependentOfPhysicalExtent = true;
		plotSurface->YAxis1->TickTextNextToAxis = false;
		plotSurface->YAxis1->TicksAngle = 3.0f * (float)Math::PI / 2.0f;
		((LinearAxis^)plotSurface->YAxis1)->LargeTickStep = 10.0;
		((LinearAxis^)plotSurface->YAxis1)->NumberOfSmallTicks = 0;

		// x axis
		plotSurface->XAxis1->TicksIndependentOfPhysicalExtent = true;
		plotSurface->XAxis1->TickTextNextToAxis = false;
		plotSurface->XAxis1->TicksAngle = (float)Math::PI / 2.0f;
		LabelAxis^ la = gcnew LabelAxis( plotSurface->XAxis1 );
		for (int i=0; i<info->Rows->Count; ++i)
		{
			la->AddLabel( (System::String^)info->Rows[i]["StoreName"], Convert::ToInt32(info->Rows[i]["Index"]) );
		}
		la->TicksLabelAngle = (float)90.0f;
		la->TicksBetweenText = true;
		plotSurface->XAxis1 = la;

		plotSurface->XAxis2 = (Axis^)plotSurface->XAxis1->Clone();
		plotSurface->XAxis2->HideTickText = true;
		plotSurface->XAxis2->LargeTickSize = 0;

		Legend^ l = gcnew Legend();
		l->NumberItemsVertically = 2;
		l->AttachTo( NPlot::PlotSurface2D::XAxisPosition::Bottom, NPlot::PlotSurface2D::YAxisPosition::Left );
		l->HorizontalEdgePlacement = NPlot::Legend::Placement::Outside;
		l->VerticalEdgePlacement = NPlot::Legend::Placement::Inside;
		l->XOffset = 5;
		l->YOffset = 50;
		l->BorderStyle = NPlot::LegendBase::BorderType::Line;

		plotSurface->Legend = l;

		plotSurface->Title = 
			"Sales Growth Compared to\n" +
			"Average Sales Growth by Store Size - Rank Order Low to High";
		
		plotSurface->Refresh();
	}
	#pragma endregion
	#pragma region PlotProfitLoss
	System::Void CPlotSurface2DDemo::PlotProfitLoss()
	{
		// this example will in the future demonstrate histogram plots with +- values.
		// currently not used - histograms don't support this.
		plotSurface->Clear();

		array<int>^ values = gcnew array<int>{-10,2,-3, 4, 6, -1, 10, 4, -4, -3 };
		HistogramPlot^ hp = gcnew HistogramPlot();
		hp->OrdinateData = values;
		plotSurface->Add(hp);

		plotSurface->Refresh();
	}
	#pragma endregion
}
