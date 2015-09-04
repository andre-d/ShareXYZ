﻿#region License Information (GPL v3)

/*
    ShareXYZ - A program that allows you to take screenshots and share any file type

    Copyright (c) 2015 ShareXYZ Team
    Copyright (c) 2007-2015 ShareX Team

    This program is free software; you can redistribute it and/or
    modify it under the terms of the GNU General Public License
    as published by the Free Software Foundation; either version 2
    of the License, or (at your option) any later version.

    This program is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with this program; if not, write to the Free Software
    Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301, USA.

    Optionally you can also view the license at <http://www.gnu.org/licenses/>.
*/

#endregion License Information (GPL v3)

using ShareXYZ.HelpersLib;
using System.IO;

namespace ShareXYZ.UploadersLib.FileUploaders
{
    public class SharedFolderUploader : FileUploader
    {
        private LocalhostAccount account;

        public SharedFolderUploader(LocalhostAccount account)
        {
            this.account = account;
        }

        public override UploadResult Upload(Stream stream, string fileName)
        {
            UploadResult result = new UploadResult();

            string filePath = account.GetLocalhostPath(fileName);

            Helpers.CreateDirectoryIfNotExist(filePath);

            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                if (TransferData(stream, fs))
                {
                    result.URL = account.GetUriPath(Path.GetFileName(fileName));
                }
            }

            return result;
        }
    }
}