﻿using System;
using System.IO;
using System.Windows.Forms;
using static DSPRE.RomInfo;

namespace DSPRE.ROMFiles {
    public abstract class RomFile {
        public abstract byte[] ToByteArray();
        public void SaveToFile(string path, bool showSuccessMessage = true) {
            
            byte[] romFileToByteArray = ToByteArray();
            if (romFileToByteArray is null) {
                Console.WriteLine(GetType().Name + " couldn't be saved!");
                return;
            }

            File.WriteAllBytes(path, romFileToByteArray);

            if (showSuccessMessage) {
                MessageBox.Show(GetType().Name + " saved successfully!", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        protected internal void SaveToFileDefaultDir(DirNames dir, int IDtoReplace, bool showSuccessMessage = true) {
            string path = RomInfo.gameDirs[dir].unpackedDir + "\\" + IDtoReplace.ToString("D4");
            this.SaveToFile(path, showSuccessMessage);
        }
        protected internal void SaveToFileExplorePath(string fileType, string fileExtension, string suggestedFileName, bool showSuccessMessage = true) {
            SaveFileDialog sf = new SaveFileDialog {
                Filter = fileType + ' ' + "(*." + fileExtension + '|' + "(*." + fileExtension
            };

            if (!string.IsNullOrEmpty(suggestedFileName)) {
                sf.FileName = suggestedFileName;
            }

            if (sf.ShowDialog() != DialogResult.OK) {
                return;
            }

            this.SaveToFile(sf.FileName, showSuccessMessage);
        }
    }
}
