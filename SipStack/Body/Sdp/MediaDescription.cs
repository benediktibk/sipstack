﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SipStack.Body.Sdp
{
    public class MediaDescription
    {
        public MediaDescription(Media media)
        {
            Media = media;
        }

        public Media Media { get; }
    }
}
