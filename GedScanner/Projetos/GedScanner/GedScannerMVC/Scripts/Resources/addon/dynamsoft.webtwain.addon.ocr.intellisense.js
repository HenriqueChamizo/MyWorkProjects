/*!
* Dynamsoft JavaScript Library
/*!
* Dynamsoft WebTwain Addon PDF JavaScript Intellisense
* Product: Dynamsoft Web Twain Addon
* Web Site: http://www.dynamsoft.com
*
* Copyright 2016, Dynamsoft Corporation 
* Author: Dynamsoft Support Team
* Version: 12.1
*/
var EnumDWT_OCRLanguage = {
    OCRL_ENG: "eng"
};

var EnumDWT_OCRPageSetMode = {
    OCRPSM_OSD_ONLY: 0,
    PSM_AUTO_OSD: 1,
    PSM_AUTO_ONLY: 2,
    PSM_AUTO: 3,
    PSM_SINGLE_COLUMN: 4,
    PSM_SINGLE_BLOCK_VERT_TEXT: 5,
    PSM_SINGLE_BLOCK: 6,
    PSM_SINGLE_LINE: 7,
    PSM_SINGLE_WORD: 8,
    PSM_CIRCLE_WORD: 9,
    PSM_SINGLE_CHAR: 10
};

var EnumDWT_OCROutputFormat = {
    OCROF_TEXT: 0,
    OCROF_PDFPLAINTEXT: 1,
    OCROF_PDFIMAGEOVERTEXT: 2,
    OCROF_PDFPLAINTEXT_PDFX: 3,
    OCROF_PDFIMAGEOVERTEXT_PDFX: 4
};


var OCR = {};
WebTwainAddon.OCR = OCR;

OCR.Download = function(remoteFile, optionalAsyncSuccessFunc, optionalAsyncFailureFunc) {
    /// <summary>Downloads and installs the ocr add-on on the local system. </summary>
    /// <param name="remoteFile" type="string">specifies the value of which frame to get.</param>
    /// <param name="optionalAsyncSuccessFunc" type="function">optional. The function to call when the download succeeds. Please refer to the function prototype OnSuccess.</param>
    /// <param name="optionalAsyncFailureFunc" type="function">optional. The function to call when the download fails. Please refer to the function prototype OnFailure.</param>
    /// <returns type="bool"></returns>   
};

OCR.DownloadLangData = function(remoteFile, optionalAsyncSuccessFunc, optionalAsyncFailureFunc) {
    /// <summary>Downloads and deploys the OCR language package on the local system.  </summary>
    /// <param name="remoteFile" type="string">specifies the value of which frame to get.</param>
    /// <param name="optionalAsyncSuccessFunc" type="function">optional. The function to call when the download succeeds. Please refer to the function prototype OnSuccess.</param>
    /// <param name="optionalAsyncFailureFunc" type="function">optional. The function to call when the download fails. Please refer to the function prototype OnFailure.</param>
    /// <returns type="bool"></returns>   
};

OCR.Recognize = function(sImageIndex, AsyncSuccessFunc, AsyncFailureFunc) {
    /// <summary> Performs OCR on a given image. </summary>
    /// <param name="sImageIndex" type="short">specifies the index of the image.</param>
    /// <param name="AsyncSuccessFunc" type="function"> The function to call when OCR operation succeeds. Please refer to the function prototype OnOCRSuccess.</param>
    /// <param name="AsyncFailureFunc" type="function"> The function to call when OCR operation fails. Please refer to the function prototype OnOCRFailure.</param>
    /// <returns type="bool"></returns>   
};

OCR.RecognizeRect = function(sImageIndex, left, top, right, bottom, AsyncSuccessFunc, AsyncFailureFunc) {
    /// <summary> Peforms OCR on the given rectangle on a specified image. </summary>
    /// <param name="sImageIndex" type="short">Specifies the index of the image.</param>
    /// <param name="left" type="int">specifies the x-coordinate of the upper-left corner of the rectangle.</param>
    /// <param name="top" type="int">specifies the y-coordinate of the upper-left corner of the rectangle.</param>
    /// <param name="right" type="int">specifies the x-coordinate of the lower-right corner of the rectangle.</param>
    /// <param name="bottom" type="int">specifies the y-coordinate of the lower-right corner of the rectangle.</param>
    /// <param name="AsyncSuccessFunc" type="function"> The function to call when OCR operation succeeds. Please refer to the function prototype OnOCRSuccess.</param>
    /// <param name="AsyncFailureFunc" type="function"> The function to call when OCR operation fails. Please refer to the function prototype OnOCRFailure.</param>
    /// <returns type="bool"></returns>   
};

OCR.RecognizeFile = function(fileNames, AsyncSuccessFunc, AsyncFailureFunc) {
    /// <summary>Performs OCR on one or multiple specified local file(s) directly. </summary>
    /// <param name="fileNames" type="short">Specifies the local paths of the target files. If multiple files are given, they should be separated by the '|' character.</param>
    /// <param name="AsyncSuccessFunc" type="function"> The function to call when OCR operation succeeds. Please refer to the function prototype OnOCRSuccess.</param>
    /// <param name="AsyncFailureFunc" type="function"> The function to call when OCR operation fails. Please refer to the function prototype OnOCRFailure.</param>
    /// <returns type="bool"></returns>   
};

OCR.RecognizeSelectedImages = function(AsyncSuccessFunc, AsyncFailureFunc) {
    /// <summary> Performs OCR on the currently selected images in the buffer. </summary>
    /// <param name="AsyncSuccessFunc" type="function"> The function to call when OCR operation succeeds. Please refer to the function prototype OnOCRSuccess.</param>
    /// <param name="AsyncFailureFunc" type="function"> The function to call when OCR operation fails. Please refer to the function prototype OnOCRFailure.</param>
    /// <returns type="bool"></returns>   
};


OCR.SetUnicodeFontName = function(name) {
	/// <summary>Specifies a font to be used by OCR when Addon.OCR.SetIfUseDetectedFont is set to false. </summary> 
	/// <param name="name" type="string">Specifies a font to be used by OCR. </param>
	/// <returns type="bool"></returns>
};

OCR.GetUnicodeFontName = function() {
	/// <summary>Returns the detected OCR font name. </summary> 
	/// <returns type="string">Returns the detected OCR font name.</returns>
};

OCR.SetIfUseDetectedFont = function(value) {
	/// <summary>Determines whether PDF output should use the fonts detected by the OCR system, or the default/provided fonts instead.</summary> 
	/// <param name="value" type="bool">By default this is true, indicating detected fonts should be used. The detected fonts must exist on the user's system for this to be successful.</param>
	/// <returns type="bool"></returns>
};

OCR.GetIfUseDetectedFont = function() {
	/// <summary>Returns whether PDF output should use the fonts detected by the OCR system, or the default/provided fonts instead.</summary> 
	/// <returns type="bool">Returns whether PDF output should use the fonts detected by the OCR system, or the default/provided fonts instead.</returns>
};

OCR.SetMinFontSizeforMoreAccurateResult = function(value) {
	/// <summary>Applies higher-level accuracy of OCR to the area of the image where the font size is bigger than the value set here. </summary> 
	/// <param name="value" type="int">Specifies the font size base to apply the higher-level accracy OCR. The default value is 0 which means no regional accurate OCR is performed.</param>
	/// <returns type="bool"></returns>
};

OCR.GetMinFontSizeforMoreAccurateResult = function() {
	/// <summary>Returns the font size base to apply higher-level regional accarate OCR which is set through Addon.OCR.SetMinFontSizeforMoreAccurateResult. </summary> 
	/// <returns type="bool">Returns the font size base to apply higher-level regional accarate OCR. If the return value is 0, it indicates no regional accurate OCR is performed. </returns>
};

OCR.SetLanguage = function(value) {
    /// <summary>Sets the target language for OCR operations. </summary>
    /// <param name="value" type="string">Specifies the target language for OCR operation. </param>
    /// <returns type="bool"></returns>   
};

OCR.SetPageSetMode = function(value) {
    /// <summary>Sets the mode for OCR page layout analysis. Determines how pages are determined when processing OCR. </summary>
    /// <param name="value" type="EnumDWT_OCRPageSetMode">Specifies the OCR Page layout analysis mode. </param>
    /// <returns type="bool"></returns>   
};

OCR.SetOutputFormat = function(value) {
    /// <summary>Sets the OCR result format. Determines whether the OCR output is in text or PDF format. </summary>
    /// <param name="value" type="EnumDWT_OCROutputFormat">Specifies the OCR result format.</param>
    /// <returns type="bool"></returns>   
};

