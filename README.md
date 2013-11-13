MD5 ZIP / Folder Check
======================

The purpose of this utility is to check if folder contents can be validated to a ZIP file which is also validated against a MD5 hash. As an example it can verify the folder contents of a cloud storage against a validated ZIP on your computer.
This utility provides a graphical user interface (if it is started without command line options) and a command line interface, so it can be used in a batch file. Currently it only compares the minimal existing files and will not care about non available files (e.g. existing within the ZIP file and not in the compare folder and vice versa).


Command line usage
==================

The executable has to be started with three arguments. The MD5 hash, which will be used to validate the ZIP file, the full path to the ZIP file and the full path to the folder which is compared to the contents of the ZIP file:

md5zipfoldercheck --md5 <MD5Hash> -z <full path to the ZIP file> -f <full path to compare folder>

Example: md5zipfoldercheck --md5 AABBCCEEFFGGHHIIJJKKLLMMNNOOPPQQ -z c:\test\test.zip -f c:\comparefolder


License
=======

This utility is released under the MIT license. See LICENSE.txt for more information.

2013 by Tim Riemann