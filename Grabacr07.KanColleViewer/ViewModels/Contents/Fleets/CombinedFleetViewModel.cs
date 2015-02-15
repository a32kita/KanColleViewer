﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grabacr07.KanColleWrapper.Models;
using Livet;

namespace Grabacr07.KanColleViewer.ViewModels.Contents.Fleets
{
	public class CombinedFleetViewModel : ViewModel
	{
		public CombinedFleet Source { get; private set; }

		public string Name
		{
			get { return this.Source.Name; }
		}

		public FleetStateViewModel State { get; private set; }

		public ViewModel QuickStateView
		{
			get
			{
				return this.Source.State.Situation.HasFlag(FleetSituation.Sortie)
					? this.State.Sortie
					: this.State.Homeport as QuickStateViewViewModel;
			}
		}

		public CombinedFleetViewModel(CombinedFleet fleet)
		{
			this.Source = fleet;

			this.State = new FleetStateViewModel(fleet.State);
			this.CompositeDisposable.Add(this.State);
		}
	}
}