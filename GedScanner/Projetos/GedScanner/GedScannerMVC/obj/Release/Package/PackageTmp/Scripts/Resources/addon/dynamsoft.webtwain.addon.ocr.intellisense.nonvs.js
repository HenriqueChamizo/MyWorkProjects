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

/**
 *  Downloads and installs the ocr add-on on the local system. 
 * @method Dynamsoft.WebTwain#Download 
 * @param {string} remoteFile specifies the value of which frame to get.
 * @param {function} optionalAsyncSuccessFunc optional. The function to call when the download succeeds. Please refer to the function prototype OnSuccess.
 * @param {function} optionalAsyncFailureFunc optional. The function to call when the download fails. Please refer to the function prototype OnFailure.
 * @return {bool}
 */
OCR.Download = function(remoteFile, optionalAsyncSuccessFunc, optionalAsyncFailureFunc) {
};

/**
 *  Downloads and deploys the OCR language package on the local system. 
 * @method Dynamsoft.WebTwain#DownloadLangData 
 * @param {string} remoteFile specifies the value of which frame to get.
 * @param {function} optionalAsyncSuccessFunc optional. The function to call when the download succeeds. Please refer to the function prototype OnSuccess.
 * @param {function} optionalAsyncFailureFunc optional. The function to call when the download fails. Please refer to the function prototype OnFailure.
 * @return {bool}
 */
OCR.DownloadLangData = function(remoteFile, optionalAsyncSuccessFunc, optionalAsyncFailureFunc) {
};

/**
 *  Performs OCR on a given image. 
 * @method Dynamsoft.WebTwain#Read 
 * @param {short} sImageIndex Specifies the index of the image.
 * @param {function} AsyncSuccessFunc  The function to call when OCR operation succeeds. Please refer to the function prototype OnOCRSuccess.
 * @param {function} AsyncFailureFunc  The function to call when OCR operation fails. Please refer to the function prototype OnOCRFailure.
 * @return {bool}
 */
OCR.Recognize = function(sImageIndex, optionalAsyncSuccessFunc, optionalAsyncFailureFunc) {
};

/**
 *  Peforms OCR on the given rectangle on a specified image. 
 * @method Dynamsoft.WebTwain#ReadRect 
 * @param {short} sImageIndex Specifies the index of the image.
 * @param {int} left specifies the x-coordinate of the upper-left corner of the rectangle.
 * @param {int} top specifies the y-coordinate of the upper-left corner of the rectangle.
 * @param {int} right specifies the x-coordinate of the lower-right corner of the rectangle.
 * @param {int} bottom specifies the y-coordinate of the lower-right corner of the rectangle.
 * @param {function} AsyncSuccessFunc  The function to call when OCR operation succeeds. Please refer to the function prototype OnOCRSuccess.
 * @param {function} AsyncFailureFunc  The function to call when OCR operation fails. Please refer to the function prototype OnOCRFailure.
 * @return {bool}
 */
OCR.RecognizeRect = function(sImageIndex, left, top, right, bottom, optionalAsyncSuccessFunc, optionalAsyncFailureFunc) {
};

/**
 *   Performs OCR on one or multiple specified local file(s) directly.
 * @method Dynamsoft.WebTwain#Read 
 * @param {short} fileNames Specifies the local paths of the target files. If multiple files are given, they should be separated by the '|' character.
 * @param {function} AsyncSuccessFunc  The function to call when OCR operation succeeds. Please refer to the function prototype OnOCRSuccess.
 * @param {function} AsyncFailureFunc  The function to call when OCR operation fails. Please refer to the function prototype OnOCRFailure.
 * @return {bool}
 */
OCR.RecognizeFile = function(fileNames, optionalAsyncSuccessFunc, optionalAsyncFailureFunc) {
};

/**
 *   Performs OCR on the currently selected images in the buffer.
 * @method Dynamsoft.WebTwain#Read 
 * @param {function} AsyncSuccessFunc  The function to call when OCR operation succeeds. Please refer to the function prototype OnOCRSuccess.
 * @param {function} AsyncFailureFunc  The function to call when OCR operation fails. Please refer to the function prototype OnOCRFailure.
 * @return {bool}
 */
OCR.RecognizeSelectedImages = function(optionalAsyncSuccessFunc, optionalAsyncFailureFunc) {
};

/**
 *  Specifies a font to be used by OCR when Addon.OCR.SetIfUseDetectedFont is set to false.  
 * @method Dynamsoft.WebTwain#SetUnicodeFontName 
 * @param {string} name Specifies a font to be used by OCR.
 * @return {bool}
 */
OCR.SetUnicodeFontName = function(name) {
};

/**
 *  Returns the detected OCR font name. 
 * @method Dynamsoft.WebTwain#GetUnicodeFontName 
 * @return {string} Returns the detected OCR font name.
 */
OCR.GetUnicodeFontName = function() {
};

/**
 *  Determines whether PDF output should use the fonts detected by the OCR system, or the default/provided fonts instead.
 * @method Dynamsoft.WebTwain#SetIfUseDetectedFont 
 * @param {bool} value By default this is true, indicating detected fonts should be used. The detected fonts must exist on the user's system for this to be successful.
 * @return {bool}
 */
OCR.SetIfUseDetectedFont = function(value) {
};

/**
 *  Returns whether PDF output should use the fonts detected by the OCR system, or the default/provided fonts instead.
 * @method Dynamsoft.WebTwain#GetIfUseDetectedFont 
 * @return {bool} Returns whether PDF output should use the fonts detected by the OCR system, or the default/provided fonts instead.
 */
OCR.GetIfUseDetectedFont = function() {
};

/**
 *  Applies higher-level accuracy of OCR to the area of the image where the font size is bigger than the value set here.
 * @method Dynamsoft.WebTwain#SetIfUseDetectedFont 
 * @param {int} value Specifies the font size base to apply the higher-level accracy OCR. The default value is 0 which means no regional accurate OCR is performed.
 * @return {bool}
 */
OCR.SetMinFontSizeforMoreAccurateResult = function(value) {
};

/**
 *  Returns the font size base to apply higher-level regional accarate OCR which is set through Addon.OCR.SetMinFontSizeforMoreAccurateResult.
 * @method Dynamsoft.WebTwain#GetMinFontSizeforMoreAccurateResult 
 * @return {bool} Returns the font size base to apply higher-level regional accarate OCR. If the return value is 0, it indicates no regional accurate OCR is performed.
 */
OCR.GetMinFontSizeforMoreAccurateResult = function() {
};

/**
 *  Sets the target language for OCR operations.
 * @method Dynamsoft.WebTwain#SetLanguage 
 * @param {string} value Specifies the target language for OCR operation.
 * @return {bool}
 */
OCR.SetLanguage = function(value) {
};

/**
 *  Sets the mode for OCR page layout analysis. Determines how pages are determined when processing OCR.
 * @method Dynamsoft.WebTwain#SetPageSetMode 
 * @param {EnumDWT_OCRPageSetMode} value Specifies the OCR Page layout analysis mode. 
 * @return {bool}
 */
OCR.SetPageSetMode = function(value) {
};

/**
 * Sets the OCR result format. Determines whether the OCR output is in text or PDF format.
 * @method Dynamsoft.WebTwain#SetOutputFormat 
 * @param {EnumDWT_OCROutputFormat} value Specifies the OCR result format.
 * @return {bool}
 */
OCR.SetOutputFormat = function(value) {
};


