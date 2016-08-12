using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Authlib.nMinecraft
{
    public class MinecraftProfileTexture
    {
        private readonly String url;

        public MinecraftProfileTexture(String url) {
            this.url = url;
        }

        public String getUrl() {
            return this.url;
        }

        public String getHash() {
            return FilenameUtils.getBaseName(this.url);
        }

        public String toString() {
            return (new StringBuilder(this)).append("url", this.url).append("hash", this.getHash()).toString();
        }

        public static enum Type {
            SKIN = 0,
            CAPE = 1
        }
    }
}
