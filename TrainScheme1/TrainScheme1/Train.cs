﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainScheme1
{
    class Train
    {
        /// <summary>
        /// name this train
        /// </summary>
        public string name;
        private bool intercity;
        private Rail rail;
        private string HEX;

        private Station[] route;
        private int nextDestination = 0;
        private bool inStation;

        private Wagon[] wagons;

        private Random r = new Random();

        /// <summary>
        /// Constructs a new train opbject
        /// </summary>
        /// <param name="name">name of the new train</param>
        /// <param name="HEX">indicator color of train</param>
        /// <param name="intercity">if true new train is an intercity</param>
        /// <param name="route">the route the train needs to take</param>
        public Train(string name, string HEX, bool intercity, Station[] route)
        {
            this.name = name;
            this.HEX = HEX;
            this.intercity = intercity;
            this.route = route;
            rail = route[0].GetRail();

            if (intercity)
            {
                wagons = new Wagon[3];
            }
            else
            {
                wagons = new Wagon[2];
            }
            

            for (int w = 0; w < wagons.Length; w++)
            {
                wagons[w] = new Wagon(rail, this);
            }

        }

        /// <summary>
        /// Remove train from old rail and Add train to new rail
        /// </summary>
        /// <param name="rail">new rail</param>
        public void SetRail(Rail rail)
        {
            this.rail.RemoveTrain(this);
            this.rail.AddWagon(wagons, wagons[0]);
            this.rail = rail;
            this.rail.AddTrain(this);

        }

        /// <summary>
        /// Sets next destination and loops back to start when cycle has been completed
        /// </summary>
        private void SetNextDestination()
        {
            nextDestination++;
            if (nextDestination == route.Length - 1)
            {
                nextDestination = 0;
            }
        }

        public bool Arrived()
        {
            return route[nextDestination].Arrived(this);
        }

        /// <summary>
        /// this train has arrived on a station
        /// </summary>
        public void Arrive()
        {
            inStation = true;
            SetNextDestination();
            rail.GetStation().AddTrain(this);
        }

        /// <summary>
        /// this train leaves the station
        /// </summary>
        public void Depart()
        {

            for (int w = 0; w < wagons.Length; w++)
            {
                wagons[w].SetRandomCroudLevel(r.Next(0, 3));
            }
            inStation = false;
        }

        /// <summary>
        /// Checks if train needs to go right to reach the next destination on his route
        /// </summary>
        /// <returns></returns>
        public bool NeedsToGoRight()
        {
            return rail.GetIndex() < route[nextDestination].GetRailIndex();
        }

        /// <summary>
        /// Gets current rail object that train is on
        /// </summary>
        /// <returns>Rail object</returns>
        public Rail GetRail()
        {
            return rail;
        }

        /// <summary>
        /// retruns true if this train is in station
        /// </summary>
        /// <returns></returns>
        public bool InStation()
        {
            return inStation;
        }

        /// <summary>
        /// retruns true if this train is an Intercity
        /// </summary>
        /// <returns></returns>
        public bool Intercity()
        {
            return intercity;
        }

        /// <summary>
        /// retruns HEX value of this trian
        /// </summary>
        /// <returns></returns>
        public string GetHEX()
        {
            return HEX;
        }

        /// <summary>
        /// Gets all wagons of this train
        /// </summary>
        /// <returns></returns>
        public Wagon[] GetWagons()
        {
            return wagons;
        }
    }
}
