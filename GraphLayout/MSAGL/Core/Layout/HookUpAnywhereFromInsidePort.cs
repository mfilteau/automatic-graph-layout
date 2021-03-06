/*
Microsoft Automatic Graph Layout,MSAGL 

Copyright (c) Microsoft Corporation

All rights reserved. 

MIT License 

Permission is hereby granted, free of charge, to any person obtaining
a copy of this software and associated documentation files (the
""Software""), to deal in the Software without restriction, including
without limitation the rights to use, copy, modify, merge, publish,
distribute, sublicense, and/or sell copies of the Software, and to
permit persons to whom the Software is furnished to do so, subject to
the following conditions:

The above copyright notice and this permission notice shall be
included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED *AS IS*, WITHOUT WARRANTY OF ANY KIND,
EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
*/
﻿using System;
using Microsoft.Msagl.Core.Geometry;
using Microsoft.Msagl.Core.Geometry.Curves;

namespace Microsoft.Msagl.Core.Layout {
    /// <summary>
    /// This port is for an edge connecting a node inside of the curve going out of the curve and creating a hook to 
    /// connect to the curve
    /// </summary>
    public class HookUpAnywhereFromInsidePort : Port {
        Func<ICurve> curve;
        double adjustmentAngle = Math.PI / 10;
        /// <summary>
        /// </summary>
        /// <param name="boundaryCurve"></param>
        /// <param name="hookSize"></param>
        public HookUpAnywhereFromInsidePort(Func<ICurve> boundaryCurve, double hookSize) {
            curve = boundaryCurve;
            HookSize=hookSize;
        }

        /// <summary>
        /// </summary>
        /// <param name="boundaryCurve"></param>
        public HookUpAnywhereFromInsidePort(Func<ICurve> boundaryCurve) {
            ValidateArg.IsNotNull(boundaryCurve,"boundaryCurve");
            curve = boundaryCurve;
            location=curve().Start;
        }

        Point location;
        /// <summary>
        /// returns a point on the boundary curve
        /// </summary>
        public override Point Location {
            get { return location; }
        }

        /// <summary>
        /// Gets the boundary curve of the port.
        /// </summary>
        public override ICurve Curve {
            get { return curve(); }
            set { throw new InvalidCastException(); }
        }

        internal void SetLocation(Point p) { location = p; }
        internal Polyline LoosePolyline { get; set; }

        /// <summary>
        /// We are trying to correct the last segment of the polyline by make it perpendicular to the Port.Curve.
        ///For this purpose we trim the curve by the cone of the angle 2*adjustment angle and project the point before the last of the polyline to this curve.
        /// </summary>
        public double AdjustmentAngle {
            get {
                return adjustmentAngle;
            }
            set {
                adjustmentAngle = value;
            }
        }
        double hookSize = 9;
        /// <summary>
        /// the size of the self-loop
        /// </summary>
        public double HookSize { get { return hookSize; } set { hookSize = value; } }
    }
}