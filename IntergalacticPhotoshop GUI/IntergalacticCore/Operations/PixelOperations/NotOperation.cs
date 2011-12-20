﻿namespace IntergalacticCore.Operations.PixelOperations
{
    using IntergalacticCore.Data;

    /// <summary>
    /// Inverts all pixels contained in this image.
    /// </summary>
    public class NotOperation : BaseOperation
    {
        /// <summary>
        /// Returns the title of the operaion
        /// </summary>
        /// <returns>The title</returns>
        public override string ToString()
        {
            return "Color Inverse";
        }

        /// <summary>
        /// Does the actual operation to the specified image.
        /// </summary>
        protected unsafe override void Operate()
        {
            IntergalacticCpp.NotOperation.Execute(this.GetCppData(this.Image));
        }
    }
}
