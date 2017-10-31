/*!
* Dynamsoft JavaScript Library
* Product: Dynamsoft Web Twain
* Web Site: http://www.dynamsoft.com
*
* Copyright 2016, Dynamsoft Corporation 
* Author: Dynamsoft R&D Department
*
* Version: 12.1
*
* Module: ibm
* final js: build\addon\dynamsoft.webtwain.addon.ibm.js
*/
(function(a){function b(c){var d=Dynamsoft.Lib.html5.Funs;c.GetFileSize=function(e){var h=this,g;if(!Dynamsoft.Lib.isString(e)){a.Errors.InvalidFile(h);return false}g=d.replaceLocalFilename(e);return this._innerFun("GetFileSize",d.makeParams(g))};c.EncodeAsBase64=function(g,e){var j=this,h;var i=function(f){if(Dynamsoft.Lib.isFunction(e)){var k=f;if(Dynamsoft.Lib.isArray(k)){if(k.length>1){k=k.slice(0,1)}}e(g,k,j.ErrorCode,j.ErrorString)}return true};if(!Dynamsoft.Lib.isString(g)){a.Errors.InvalidFile(j);i("");return false}h=d.replaceLocalFilename(g);return this._innerSend("EncodeAsBase64",d.makeParams(h),true,i,i)};c.DeleteFile=function(e){var h=this,g;if(!Dynamsoft.Lib.isString(e)){a.Errors.InvalidFile(h);return false}g=d.replaceLocalFilename(e);return this._innerFun("DWTDeleteFile",d.makeParams(g))};c.DeleteFolder=function(e){var h=this,g;if(!Dynamsoft.Lib.isString(e)){a.Errors.InvalidFile(h);return false}g=d.replaceLocalFilename(e);return this._innerFun("DWTDeleteFolder",d.makeParams(g))};c.CopyFile=function(k,f,e){var j=this,g,i;var h=function(){if(Dynamsoft.Lib.isFunction(e)){e(k,f,j.ErrorCode,j.ErrorString)}return true};if(!Dynamsoft.Lib.isString(k)){a.Errors.InvalidSourceFile(j);h();return false}if(!Dynamsoft.Lib.isString(f)){a.Errors.InvalidDestination(j);h();return false}g=d.replaceLocalFilename(k);i=d.replaceLocalFilename(f);return this._innerSend("DWTCopyFile",d.makeParams(g,i),true,h,h)};c.TiffPageCount=function(g){var i=this,h,e=0;if(!Dynamsoft.Lib.isString(g)){a.Errors.InvalidFile(i);return e}h=d.replaceLocalFilename(g);e=this._innerFun("TiffPageCount",d.makeParams(h));if(e<0){e=0}return e};c.SplitTiff=function(i,f,e,g){var l=this,h,k;var j=function(n){if(Dynamsoft.Lib.isFunction(g)){var m=0;if(l.ErrorCode==0){if(n&&n.length>=1){m=n[0]}}g(i,f,e,m,l.ErrorCode,l.ErrorString)}return true};if(!Dynamsoft.Lib.isString(i)){a.Errors.InvalidSourceFile(l);j(false);return false}if(!Dynamsoft.Lib.isString(e)){a.Errors.InvalidDestination(l);j(false);return false}h=d.replaceLocalFilename(i);k=d.replaceLocalFilename(e);return this._innerSend("SplitTiff",d.makeParams(h,f,k),true,j,j)};c.MergeTiff=function(g,k,n){var j=this,f=false,l;var m=function(){if(Dynamsoft.Lib.isFunction(n)){n(g,k,j.ErrorCode,j.ErrorString)}return true};if(a.isArray(g)){for(var h=0;h<g.length;h++){var e=g[h];if(!a.isString(e)||e.length==0){a.Errors.InvalidSourceFile(j);m();return false}}f=d.replaceLocalFilename(g.join(","))}else{a.Errors.InvalidSourceFile(j);m();return false}if(!a.isString(k)){a.Errors.InvalidDestination(j);m();return false}l=d.replaceLocalFilename(k);return this._innerSend("MergeTiff",d.makeParams(f,l),true,m,m)};c.ShowFileDialog=function(i,g,p,n,m,e,h,q,l){var k=this,j=d.replaceLocalFilename(m);var o=function(t){if(a.isFunction(l)){var f=[];for(var r=0;r<t.length;r++){var s=a.base64.decode(t[r]);f.push(s)}l(f,k.ErrorCode,k.ErrorString)}return true};if(a.isFunction(l)){q=q+100}return this._innerSend("ShowFileDialog",d.makeParams(i,g,p,n,j,e,h,q),true,o,o)};c.GetCapDescription=function(t){var m=this;var g=[];m.OpenSource();m.Capability=t;m.CapGet();var h=m.CapValueType;var n=m.CapNumItems;var k=0;switch(h){case EnumDWT_CapValueType.TWTY_INT8:case EnumDWT_CapValueType.TWTY_INT8:case EnumDWT_CapValueType.TWTY_INT16:case EnumDWT_CapValueType.TWTY_INT32:case EnumDWT_CapValueType.TWTY_UINT8:case EnumDWT_CapValueType.TWTY_UINT16:case EnumDWT_CapValueType.TWTY_UINT32:case EnumDWT_CapValueType.TWTY_BOOL:for(k=0;k<n;k++){g.push(m.__convertCAP_Item_toString(t,m.GetCapItems(k),h))}break;case EnumDWT_CapValueType.TWTY_STR32:case EnumDWT_CapValueType.TWTY_STR64:case EnumDWT_CapValueType.TWTY_STR128:case EnumDWT_CapValueType.TWTY_STR255:for(k=0;k<n;k++){g.push(m.GetCapItems(k))}break;case EnumDWT_CapValueType.TWTY_FIX32:for(k=0;k<n;k++){g.push(m.GetCapItems(k))}break;case EnumDWT_CapValueType.TWTY_FRAME:for(k=0;k<n;k++){var l=m.CapGetFrameLeft(k);var r=m.CapGetFrameRight(k);var j=m.CapGetFrameTop(k);var e=m.CapGetFrameBottom(k);var p=parseInt(l)==l?l.toFixed(1):l;var q=parseInt(r)==r?r.toFixed(1):r;var f=parseInt(j)==j?j.toFixed(1):j;var s=parseInt(e)==e?e.toFixed(1):e;var o=p+" "+q+" "+f+" "+s;g.push(o)}break}return g};c.__convertCAP_Item_toString=function(e,g,i){var h=this;var f="Unknown capability";switch(e){case EnumDWT_Cap.CAP_ALARMS:f=h.__convertCAP_ALARMS_toString(g);break;case EnumDWT_Cap.ICAP_AUTOSIZE:f=h.__convertICAP_AUTOSIZE_toString(g);break;case EnumDWT_Cap.ICAP_COMPRESSION:f=h.__convertICAP_COMPRESSION_toString(g);break;case EnumDWT_Cap.ICAP_BARCODESEARCHMODE:f=h.__convertICAP_BARCODESEARCHMODE_toString(g);break;case EnumDWT_Cap.ICAP_BITORDER:f=h.__convertICAP_BITORDER_toString(g);break;case EnumDWT_Cap.ICAP_AUTODISCARDBLANKPAGES:f=h.__convertICAP_AUTODISCARDBLANKPAGES_toString(g);break;case EnumDWT_Cap.ICAP_BITDEPTH:f=g;break;case EnumDWT_Cap.ICAP_BITDEPTHREDUCTION:f=h.__convertICAP_BITDEPTHREDUCTION_toString(g);break;case EnumDWT_Cap.ICAP_SUPPORTEDBARCODETYPES:f=h.__convertICAP_SUPPORTEDBARCODETYPES_toString(g);break;case EnumDWT_Cap.CAP_CAMERASIDE:f=h.__convertCAP_CAMERASIDE_toString(g);break;case EnumDWT_Cap.CAP_CLEARBUFFERS:f=h.__convertCAP_CLEARBUFFERS_toString(g);break;case EnumDWT_Cap.CAP_DEVICEEVENT:f=h.__convertCAP_DEVICEEVENT_toString(g);break;case EnumDWT_Cap.CAP_DUPLEX:f=h.__convertCAP_DUPLEX_toString(g);break;case EnumDWT_Cap.CAP_EXTENDEDCAPS:f=h.__convertCAP_toString(g);break;case EnumDWT_Cap.CAP_FEEDERALIGNMENT:f=h.__convertCAP_FEEDERALIGNMENT_toString(g);break;case EnumDWT_Cap.ICAP_FEEDERTYPE:f=h.__convertICAP_FEEDERTYPE_toString(g);break;case EnumDWT_Cap.ICAP_IMAGEFILEFORMAT:f=h.__convertICAP_IMAGEFILEFORMAT_toString(g);break;case EnumDWT_Cap.ICAP_FLASHUSED2:f=h.__convertICAP_FLASHUSED2_toString(g);break;case EnumDWT_Cap.CAP_FEEDERORDER:f=h.__convertCAP_FEEDERORDER_toString(g);break;case EnumDWT_Cap.CAP_FEEDERPOCKET:f=h.__convertCAP_FEEDERPOCKET_toString(g);break;case EnumDWT_Cap.ICAP_FLIPROTATION:f=h.__convertICAP_FLIPROTATION_toString(g);break;case EnumDWT_Cap.ICAP_FILTER:f=h.__convertICAP_FILTER_toString(g);break;case EnumDWT_Cap.ICAP_ICCPROFILE:f=h.__convertICAP_ICCPROFILE_toString(g);break;case EnumDWT_Cap.ICAP_IMAGEFILTER:f=h.__convertICAP_IMAGEFILTER_toString(g);break;case EnumDWT_Cap.ICAP_IMAGEMERGE:f=h.__convertICAP_IMAGEMERGE_toString(g);break;case EnumDWT_Cap.CAP_JOBCONTROL:f=h.__convertCAP_JOBCONTROL_toString(g);break;case EnumDWT_Cap.ICAP_JPEGQUALITY:f=h.__convertICAP_JPEGQUALITY_toString(g);break;case EnumDWT_Cap.ICAP_LIGHTPATH:f=h.__convertICAP_LIGHTPATH_toString(g);break;case EnumDWT_Cap.ICAP_LIGHTSOURCE:f=h.__convertICAP_LIGHTSOURCE_toString(g);break;case EnumDWT_Cap.ICAP_NOISEFILTER:f=h.__convertICAP_NOISEFILTER_toString(g);break;case EnumDWT_Cap.ICAP_ORIENTATION:f=h.__convertICAP_ORIENTATION_toString(g);break;case EnumDWT_Cap.ICAP_OVERSCAN:f=h.__convertICAP_OVERSCAN_toString(g);break;case EnumDWT_Cap.ICAP_PLANARCHUNKY:f=h.__convertICAP_PLANARCHUNKY_toString(g);break;case EnumDWT_Cap.ICAP_PIXELFLAVOR:f=h.__convertICAP_PIXELFLAVOR_toString(g);break;case EnumDWT_Cap.CAP_PRINTERMODE:f=h.__convertCAP_PRINTERMODE_toString(g);break;case EnumDWT_Cap.CAP_PRINTER:f=h.__convertCAP_PRINTER_toString(g);break;case EnumDWT_Cap.CAP_POWERSUPPLY:f=h.__convertCAP_POWERSUPPLY_toString(g);break;case EnumDWT_Cap.ICAP_PIXELTYPE:f=h.__convertICAP_PIXELTYPE_toString(g);break;case EnumDWT_Cap.CAP_SEGMENTED:f=h.__convertCAP_SEGMENTED_toString(g);break;case EnumDWT_Cap.ICAP_SUPPORTEDEXTIMAGEINFO:f=h.__convertExtImageInfoName_toString(g);break;case EnumDWT_Cap.ICAP_SUPPORTEDSIZES:f=h.__convertICAP_SUPPORTEDSIZES_toString(g);break;case EnumDWT_Cap.ICAP_XFERMECH:f=h.__convertICAP_XFERMECH_toString(g);break;case EnumDWT_Cap.ICAP_UNITS:f=h.__convertICAP_UNITS_toString(g);break;default:switch(i){case EnumDWT_CapValueType.TWTY_UINT8:case EnumDWT_CapValueType.TWTY_UINT16:case EnumDWT_CapValueType.TWTY_UINT32:case EnumDWT_CapValueType.TWTY_INT8:case EnumDWT_CapValueType.TWTY_INT16:case EnumDWT_CapValueType.TWTY_INT32:f=g;break;case EnumDWT_CapValueType.TWTY_BOOL:f=g?"True":"False";break}break}return f};c.__convertCAP_ALARMS_toString=function(e){var f="";switch(e){case TWAL_ALARM:f="Alarm";break;case TWAL_FEEDERERROR:f="Feeder Error";break;case TWAL_FEEDERWARNING:f="Feeder Warning";break;case TWAL_BARCODE:f="Barcode";break;case TWAL_DOUBLEFEED:f="Double Feed";break;case TWAL_JAM:f="Paper Jam";break;case TWAL_PATCHCODE:f="Patch Code";break;case TWAL_POWER:f="Power";break;case TWAL_SKEW:f="Skew";break;default:f="Unknown TWAL "+e;break}return f};c.__convertICAP_AUTOSIZE_toString=function(e){var f="";switch(e){case 0:f="None";break;case 1:f="Auto";break;case 2:f="Current";break;default:f="Unknown TWAS "+e;break}return f};c.__convertICAP_COMPRESSION_toString=function(e){var f="";switch(e){case 0:f="None";break;case 1:f="PACKBITS";break;case 2:f="GROUP31D";break;case 3:f="GROUP31DEOL";break;case 4:f="GROUP32D";break;case 5:f="GROUP4";break;case 6:f="JPEG";break;case 7:f="LZW";break;case 8:f="JBIG";break;case 9:f="PNG";break;case 10:f="RLE4";break;case 11:f="RLE8";break;case 12:f="BITFIELDS";break;default:f="Unknown TWCP "+e;break}return f};c.__convertICAP_BARCODESEARCHMODE_toString=function(e){var f="";switch(e){case 0:f="Horizontal";break;case 1:f="Vertical";break;case 2:f="Horz Vert";break;case 3:f="Vert Horz";break;default:f="Unknown TWBD "+e;break}return f};c.__convertICAP_BITORDER_toString=function(e){var f="";switch(e){case EnumDWT_CapBitOrder.TWBO_LSBFIRST:f="LSB first";break;case EnumDWT_CapBitOrder.TWBO_MSBFIRST:f="MSB first";break;default:f="Unknown TWBO "+e;break}return f};c.__convertICAP_AUTODISCARDBLANKPAGES_toString=function(e){var f="";switch(e){case -2:f="Disable";break;case -1:f="Auto";break;default:f="Unknown TWBP "+e;break}return f};c.__convertICAP_BITDEPTHREDUCTION_toString=function(e){var f="";switch(e){case EnumDWT_CapBitdepthReduction.TWBR_THRESHOLD:f="Threshold";break;case EnumDWT_CapBitdepthReduction.TWBR_HALFTONE:f="Halftone";break;case EnumDWT_CapBitdepthReduction.TWBR_CUSTHALFTONE:f="Custom Halftone";break;case EnumDWT_CapBitdepthReduction.TWBR_DIFFUSION:f="Diffusion";break;default:f="Unknown TWBR "+e;break}return f};c.__convertICAP_SUPPORTEDBARCODETYPES_toString=function(e){var f="";switch(e){case 0:f="3of9";break;case 1:f="2of5 interleaved";break;case 2:f="2of5 noninterleaved";break;case 3:f="code93";break;case 4:f="code128";break;case 5:f="UCC128";break;case 6:f="Codebar";break;case 7:f="UPCA";break;case 8:f="UPCE";break;case 9:f="EAN8";break;case 10:f="EAN13";break;case 11:f="POSTNET";break;case 12:f="PDF417";break;case 13:f="2of5 industrial";break;case 14:f="2of5 matrix";break;case 15:f="2of5 datalogic";break;case 16:f="2of5 IATA";break;case 17:f="3of9 fullASCII";break;case 18:f="Codabar with start stop";break;case 19:f="MAXICODE";break;default:f="Unknown TWBT "+e;break}return f};c.__convertCAP_CAMERASIDE_toString=function(e){var f="";switch(e){case 0:f="Both";break;case 1:f="Top";break;case 2:f="Bottom";break;default:f="Unknown TWCS "+e;break}return f};c.__convertCAP_CLEARBUFFERS_toString=function(e){var f="";switch(e){case 0:f="Auto";break;case 1:f="Clear";break;case 2:f="No Clear";break;default:f="Unknown TWCB "+e;break}return f};c.__convertCAP_DEVICEEVENT_toString=function(e){var f="";switch(e){case 32768:f="Custom Events";break;case 0:f="Check Automatic Capture";break;case 1:f="Check Battery";break;case 2:f="Check Device Online";break;case 3:f="Check Flash";break;case 4:f="Check Power Supply";break;case 5:f="Check Resolution";break;case 6:f="Device Added";break;case 7:f="Device Offline";break;case 8:f="Device Ready";break;case 9:f="Device Removed";break;case 10:f="Image Captured";break;case 11:f="Image Deleted";break;case 12:f="Paper Doublefeed";break;case 13:f="Paperjam";break;case 14:f="Lamp Failure";break;case 15:f="Power Save";break;case 16:f="Power Save Notify";break;default:f="Unknown TWDE "+e;break}return f};c.__convertCAP_DUPLEX_toString=function(e){var f="";switch(e){case EnumDWT_DUPLEX.TWDX_NONE:f="None";break;case EnumDWT_DUPLEX.TWDX_1PASSDUPLEX:f="1 Pass Duplex";break;case EnumDWT_DUPLEX.TWDX_2PASSDUPLEX:f="2 Pass Duplex";break;default:f="Unknown TWDX "+e;break}return f};c.__convertCAP_toString=function(e){var f="";switch(e){case 4404:f="ICAP_AUTODISCARDBLANKPAGES";break;case 32768:f="CAP_CUSTOMBASE";break;case 1:f="CAP_XFERCOUNT";break;case 256:f="ICAP_COMPRESSION";break;case 257:f="ICAP_PIXELTYPE";break;case 258:f="ICAP_UNITS";break;case 259:f="ICAP_XFERMECH";break;case 4096:f="CAP_AUTHOR";break;case 4097:f="CAP_CAPTION";break;case 4098:f="CAP_FEEDERENABLED";break;case 4099:f="CAP_FEEDERLOADED";break;case 4100:f="CAP_TIMEDATE";break;case 4101:f="CAP_SUPPORTEDCAPS";break;case 4102:f="CAP_EXTENDEDCAPS";break;case 4103:f="CAP_AUTOFEED";break;case 4104:f="CAP_CLEARPAGE";break;case 4105:f="CAP_FEEDPAGE";break;case 4106:f="CAP_REWINDPAGE";break;case 4107:f="CAP_INDICATORS";break;case 4108:f="CAP_SUPPORTEDCAPSEXT";break;case 4109:f="CAP_PAPERDETECTABLE";break;case 4110:f="CAP_UICONTROLLABLE";break;case 4111:f="CAP_DEVICEONLINE";break;case 4112:f="CAP_AUTOSCAN";break;case 4113:f="CAP_THUMBNAILSENABLED";break;case 4114:f="CAP_DUPLEX";break;case 4115:f="CAP_DUPLEXENABLED";break;case 4116:f="CAP_ENABLEDSUIONLY";break;case 4117:f="CAP_CUSTOMDSDATA";break;case 4118:f="CAP_ENDORSER";break;case 4119:f="CAP_JOBCONTROL";break;case 4120:f="CAP_ALARMS";break;case 4121:f="CAP_ALARMVOLUME";break;case 4122:f="CAP_AUTOMATICCAPTURE";break;case 4123:f="CAP_TIMEBEFOREFIRSTCAPTURE";break;case 4124:f="CAP_TIMEBETWEENCAPTURES";break;case 4125:f="CAP_CLEARBUFFERS";break;case 4126:f="CAP_MAXBATCHBUFFERS";break;case 4127:f="CAP_DEVICETIMEDATE";break;case 4128:f="CAP_POWERSUPPLY";break;case 4129:f="CAP_CAMERAPREVIEWUI";break;case 4130:f="CAP_DEVICEEVENT";break;case 4132:f="CAP_SERIALNUMBER";break;case 4134:f="CAP_PRINTER";break;case 4135:f="CAP_PRINTERENABLED";break;case 4136:f="CAP_PRINTERINDEX";break;case 4137:f="CAP_PRINTERMODE";break;case 4138:f="CAP_PRINTERSTRING";break;case 4139:f="CAP_PRINTERSUFFIX";break;case 4140:f="CAP_LANGUAGE";break;case 4141:f="CAP_FEEDERALIGNMENT";break;case 4142:f="CAP_FEEDERORDER";break;case 4144:f="CAP_REACQUIREALLOWED";break;case 4146:f="CAP_BATTERYMINUTES";break;case 4147:f="CAP_BATTERYPERCENTAGE";break;case 4148:f="CAP_CAMERASIDE";break;case 4149:f="CAP_SEGMENTED";break;case 4150:f="CAP_CAMERAENABLED";break;case 4151:f="CAP_CAMERAORDER";break;case 4152:f="CAP_MICRENABLED";break;case 4153:f="CAP_FEEDERPREP";break;case 4154:f="CAP_FEEDERPOCKET";break;case 4155:f="CAP_AUTOMATICSENSEMEDIUM";break;case 4156:f="CAP_CUSTOMINTERFACEGUID";break;case 4352:f="ICAP_AUTOBRIGHT";break;case 4353:f="ICAP_BRIGHTNESS";break;case 4355:f="ICAP_CONTRAST";break;case 4356:f="ICAP_CUSTHALFTONE";break;case 4357:f="ICAP_EXPOSURETIME";break;case 4358:f="ICAP_FILTER";break;case 4359:f="ICAP_FLASHUSED";break;case 4360:f="ICAP_GAMMA";break;case 4361:f="ICAP_HALFTONES";break;case 4362:f="ICAP_HIGHLIGHT";break;case 4364:f="ICAP_IMAGEFILEFORMAT";break;case 4365:f="ICAP_LAMPSTATE";break;case 4366:f="ICAP_LIGHTSOURCE";break;case 4368:f="ICAP_ORIENTATION";break;case 4369:f="ICAP_PHYSICALWIDTH";break;case 4370:f="ICAP_PHYSICALHEIGHT";break;case 4371:f="ICAP_SHADOW";break;case 4372:f="ICAP_FRAMES";break;case 4374:f="ICAP_XNATIVERESOLUTION";break;case 4375:f="ICAP_YNATIVERESOLUTION";break;case 4376:f="ICAP_XRESOLUTION";break;case 4377:f="ICAP_YRESOLUTION";break;case 4378:f="ICAP_MAXFRAMES";break;case 4379:f="ICAP_TILES";break;case 4380:f="ICAP_BITORDER";break;case 4381:f="ICAP_CCITTKFACTOR";break;case 4382:f="ICAP_LIGHTPATH";break;case 4383:f="ICAP_PIXELFLAVOR";break;case 4384:f="ICAP_PLANARCHUNKY";break;case 4385:f="ICAP_ROTATION";break;case 4386:f="ICAP_SUPPORTEDSIZES";break;case 4387:f="ICAP_THRESHOLD";break;case 4388:f="ICAP_XSCALING";break;case 4389:f="ICAP_YSCALING";break;case 4390:f="ICAP_BITORDERCODES";break;case 4391:f="ICAP_PIXELFLAVORCODES";break;case 4392:f="ICAP_JPEGPIXELTYPE";break;case 4394:f="ICAP_TIMEFILL";break;case 4395:f="ICAP_BITDEPTH";break;case 4396:f="ICAP_BITDEPTHREDUCTION";break;case 4397:f="ICAP_UNDEFINEDIMAGESIZE";break;case 4398:f="ICAP_IMAGEDATASET";break;case 4399:f="ICAP_EXTIMAGEINFO";break;case 4400:f="ICAP_MINIMUMHEIGHT";break;case 4401:f="ICAP_MINIMUMWIDTH";break;case 4406:f="ICAP_FLIPROTATION";break;case 4407:f="ICAP_BARCODEDETECTIONENABLED";break;case 4408:f="ICAP_SUPPORTEDBARCODETYPES";break;case 4409:f="ICAP_BARCODEMAXSEARCHPRIORITIES";break;case 4410:f="ICAP_BARCODESEARCHPRIORITIES";break;case 4411:f="ICAP_BARCODESEARCHMODE";break;case 4412:f="ICAP_BARCODEMAXRETRIES";break;case 4413:f="ICAP_BARCODETIMEOUT";break;case 4414:f="ICAP_ZOOMFACTOR";break;case 4415:f="ICAP_PATCHCODEDETECTIONENABLED";break;case 4416:f="ICAP_SUPPORTEDPATCHCODETYPES";break;case 4417:f="ICAP_PATCHCODEMAXSEARCHPRIORITIES";break;case 4418:f="ICAP_PATCHCODESEARCHPRIORITIES";break;case 4419:f="ICAP_PATCHCODESEARCHMODE";break;case 4420:f="ICAP_PATCHCODEMAXRETRIES";break;case 4421:f="ICAP_PATCHCODETIMEOUT";break;case 4422:f="ICAP_FLASHUSED2";break;case 4423:f="ICAP_IMAGEFILTER";break;case 4424:f="ICAP_NOISEFILTER";break;case 4425:f="ICAP_OVERSCAN";break;case 4432:f="ICAP_AUTOMATICBORDERDETECTION";break;case 4433:f="ICAP_AUTOMATICDESKEW";break;case 4434:f="ICAP_AUTOMATICROTATE";break;case 4435:f="ICAP_JPEGQUALITY";break;case 4436:f="ICAP_FEEDERTYPE";break;case 4437:f="ICAP_ICCPROFILE";break;case 4438:f="ICAP_AUTOSIZE";break;case 4439:f="ICAP_AUTOMATICCROPUSESFRAME";break;case 4440:f="ICAP_AUTOMATICLENGTHDETECTION";break;case 4441:f="ICAP_AUTOMATICCOLORENABLED";break;case 4442:f="ICAP_AUTOMATICCOLORNONCOLORPIXELTYPE";break;case 4443:f="ICAP_COLORMANAGEMENTENABLED";break;case 4444:f="ICAP_IMAGEMERGE";break;case 4445:f="ICAP_IMAGEMERGEHEIGHTTHRESHOLD";break;case 4446:f="ICAP_SUPPORTEDEXTIMAGEINFO";break;case 4609:f="ACAP_AUDIOFILEFORMAT";break;case 4610:f="ACAP_XFERMECH";break;default:if(e<CAP_CUSTOMBASE){f="Unknown CAP "+e}else{if(e>CAP_CUSTOMBASE){f="Custom CAP "+e}else{f="Invalid CAP "+e}}break}return f};c.__convertCAP_FEEDERALIGNMENT_toString=function(e){var f="";switch(e){case EnumDWT_CapFeederAlignment.TWFA_NONE:f="None";break;case EnumDWT_CapFeederAlignment.TWFA_LEFT:f="Left";break;case EnumDWT_CapFeederAlignment.TWFA_CENTER:f="Center";break;case EnumDWT_CapFeederAlignment.TWFA_RIGHT:f="Right";break;default:f="Unknown TWFA "+e;break}return f};c.__convertICAP_FEEDERTYPE_toString=function(e){var f="";switch(e){case 0:f="General";break;case 1:f="Photo";break;default:f="Unknown TWFE "+e;break}return f};c.__convertICAP_IMAGEFILEFORMAT_toString=function(e){var f="";switch(e){case EnumDWT_FileFormat.TWFF_TIFF:f="tiff";break;case EnumDWT_FileFormat.TWFF_PICT:f="pict";break;case EnumDWT_FileFormat.TWFF_BMP:f="bmp";break;case EnumDWT_FileFormat.TWFF_XBM:f="xbm";break;case EnumDWT_FileFormat.TWFF_JFIF:f="jpeg";break;case EnumDWT_FileFormat.TWFF_FPX:f="fpx";break;case EnumDWT_FileFormat.TWFF_TIFFMULTI:f="multi image tiff";break;case EnumDWT_FileFormat.TWFF_PNG:f="png";break;case EnumDWT_FileFormat.TWFF_SPIFF:f="spiff";break;case EnumDWT_FileFormat.TWFF_EXIF:f="exif";break;case EnumDWT_FileFormat.TWFF_PDF:f="PDF";break;case EnumDWT_FileFormat.TWFF_JP2:f="JP2";break;case EnumDWT_FileFormat.TWFF_JPN:f="JPN";break;case EnumDWT_FileFormat.TWFF_JPX:f="JPX";break;case EnumDWT_FileFormat.TWFF_DEJAVU:f="DEJAVU";break;case EnumDWT_FileFormat.TWFF_PDFA:f="PDF-A1";break;case EnumDWT_FileFormat.TWFF_PDFA2:f="PDF-A2";break;default:f="Unknown TWFF "+e;break}return f};c.__convertICAP_FLASHUSED2_toString=function(e){var f="";switch(e){case EnumDWT_CapFlash.TWFL_NONE:f="None";break;case EnumDWT_CapFlash.TWFL_OFF:f="Off";break;case EnumDWT_CapFlash.TWFL_ON:f="On";break;case EnumDWT_CapFlash.TWFL_AUTO:f="Auto";break;case EnumDWT_CapFlash.TWFL_REDEYE:f="Redeye";break;default:f="Unknown TWFL "+e;break}return f};c.__convertCAP_FEEDERORDER_toString=function(e){var f="";switch(e){case EnumDWT_CapFeederOrder.TWFO_FIRSTPAGEFIRST:f="First Page First";break;case EnumDWT_CapFeederOrder.TWFO_LASTPAGEFIRST:f="Last Page First";break;default:f="Unknown TWFO "+e;break}return f};c.__convertCAP_FEEDERPOCKET_toString=function(e){var f="";switch(e){case 0:f="Pocket Error";break;case 1:f="Pocket 1";break;case 2:f="Pocket 2";break;case 3:f="Pocket 3";break;case 4:f="Pocket 4";break;case 5:f="Pocket 5";break;case 6:f="Pocket 6";break;case 7:f="Pocket 7";break;case 8:f="Pocket 8";break;case 9:f="Pocket 9";break;case 10:f="Pocket 10";break;case 11:f="Pocket 11";break;case 12:f="Pocket 12";break;case 13:f="Pocket 13";break;case 14:f="Pocket 14";break;case 15:f="Pocket 15";break;case 16:f="Pocket 16";break;default:f="Unknown TWFP "+e;break}return f};c.__convertICAP_FLIPROTATION_toString=function(e){var f="";switch(e){case EnumDWT_CapFlipRotation.TWFR_BOOK:f="Book";break;case EnumDWT_CapFlipRotation.TWFR_FANFOLD:f="Fanfold";break;default:f="Unknown TWFR "+e;break}return f};c.__convertICAP_FILTER_toString=function(e){var f="";switch(e){case EnumDWT_CapFilterType.TWFT_RED:f="Red";break;case EnumDWT_CapFilterType.TWFT_GREEN:f="Green";break;case EnumDWT_CapFilterType.TWFT_BLUE:f="Blue";break;case EnumDWT_CapFilterType.TWFT_NONE:f="None";break;case EnumDWT_CapFilterType.TWFT_WHITE:f="White";break;case EnumDWT_CapFilterType.TWFT_CYAN:f="Cyan";break;case EnumDWT_CapFilterType.TWFT_MAGENTA:f="Magenta";break;case EnumDWT_CapFilterType.TWFT_YELLOW:f="Yellow";break;case EnumDWT_CapFilterType.TWFT_BLACK:f="Black";break;default:f="Unknown TWFT "+e;break}return f};c.__convertICAP_ICCPROFILE_toString=function(e){var f="";switch(e){case 0:f="None";break;case 1:f="Link";break;case 2:f="Embed";break;default:f="Unknown TWIC "+e;break}return f};c.__convertICAP_IMAGEFILTER_toString=function(e){var f="";switch(e){case EnumDWT_CapImageFilter.TWIF_NONE:f="None";break;case EnumDWT_CapImageFilter.TWIF_AUTO:f="Auto";break;case EnumDWT_CapImageFilter.TWIF_LOWPASS:f="Low Pass";break;case EnumDWT_CapImageFilter.TWIF_BANDPASS:f="Band Pass";break;case EnumDWT_CapImageFilter.TWIF_HIGHPASS:f="High Pass";break;default:f="Unknown TWIF "+e;break}return f};c.__convertICAP_IMAGEMERGE_toString=function(e){var f="";switch(e){case 0:f="None";break;case 1:f="Front on Top";break;case 2:f="Front on Bottom";break;case 3:f="Front on Left";break;case 4:f="Front on Right";break;default:f="Unknown TWIM "+e;break}return f};c.__convertCAP_JOBCONTROL_toString=function(e){var f="";switch(e){case 0:f="None";break;case 1:f="JSIC";break;case 2:f="JSIS";break;case 3:f="JSXC";break;case 4:f="JSXS";break;default:f="Unknown TWJC "+e;break}return f};c.__convertICAP_JPEGQUALITY_toString=function(e){var f="";switch(e){case -4:f="Unknown";break;case -3:f="Low";break;case -2:f="Medium";break;case -1:f="High";break;default:f="Unknown TWJC "+e;break}return f};c.__convertICAP_LIGHTPATH_toString=function(e){var f="";switch(e){case EnumDWT_CapLightPath.TWLP_REFLECTIVE:f="Reflective";break;case EnumDWT_CapLightPath.TWLP_TRANSMISSIVE:f="Transmissive";break;default:f="Unknown TWLP "+e;break}return f};c.__convertICAP_LIGHTSOURCE_toString=function(e){var f="";switch(e){case EnumDWT_CapLightSource.TWLS_RED:f="Red";break;case EnumDWT_CapLightSource.TWLS_GREEN:f="Green";break;case EnumDWT_CapLightSource.TWLS_BLUE:f="Blue";break;case EnumDWT_CapLightSource.TWLS_NONE:f="None";break;case EnumDWT_CapLightSource.TWLS_WHITE:f="White";break;case EnumDWT_CapLightSource.TWLS_UV:f="UV";break;case EnumDWT_CapLightSource.TWLS_IR:f="IR";break;default:f="Unknown TWLS "+e;break}return f};c.__convertICAP_NOISEFILTER_toString=function(e){var f="";switch(e){case EnumDWT_CapNoiseFilter.TWNF_NONE:f="None";break;case EnumDWT_CapNoiseFilter.TWNF_AUTO:f="Auto";break;case EnumDWT_CapNoiseFilter.TWNF_LONEPIXEL:f="Low Pixel";break;case EnumDWT_CapNoiseFilter.TWNF_MAJORITYRULE:f="Majoriry Rule";break;default:f="Unknown TWNF "+e;break}return f};c.__convertICAP_ORIENTATION_toString=function(e){var f="";switch(e){case EnumDWT_CapORientation.TWOR_ROT0:f="Orientation 0";break;case EnumDWT_CapORientation.TWOR_ROT90:f="Orientation 90";break;case EnumDWT_CapORientation.TWOR_ROT180:f="Orientation 180";break;case EnumDWT_CapORientation.TWOR_ROT270:f="Orientation 270";break;case EnumDWT_CapORientation.TWOR_PORTRAIT:f="Portrait";break;case EnumDWT_CapORientation.TWOR_LANDSCAPE:f="Landscape";break;case EnumDWT_CapORientation.TWOR_AUTO:f="Auto";break;case EnumDWT_CapORientation.TWOR_AUTOTEXT:f="Auto Text";break;case EnumDWT_CapORientation.TWOR_AUTOPICTURE:f="Auto Picture";break;default:f="Unknown TWOR "+e;break}return f};c.__convertICAP_OVERSCAN_toString=function(e){var f="";switch(e){case EnumDWT_CapOverscan.TWOV_NONE:f="None";break;case EnumDWT_CapOverscan.TWOV_AUTO:f="Auto";break;case EnumDWT_CapOverscan.TWOV_TOPBOTTOM:f="Top Bottom";break;case EnumDWT_CapOverscan.TWOV_LEFTRIGHT:f="Left Right";break;case EnumDWT_CapOverscan.TWOV_ALL:f="All";break;default:f="Unknown TWOV "+e;break}return f};c.__convertICAP_PLANARCHUNKY_toString=function(e){var f="";switch(e){case EnumDWT_CapPlanarChunky.TWPC_CHUNKY:f="Chunky";break;case EnumDWT_CapPlanarChunky.TWPC_PLANAR:f="Planar";break;default:f="Unknown TWPC "+e;break}return f};c.__convertICAP_PIXELFLAVOR_toString=function(e){var f="";switch(e){case EnumDWT_CapPixelFlavor.TWPF_CHOCOLATE:f="Chocolate";break;case EnumDWT_CapPixelFlavor.TWPF_VANILLA:f="Vanilla";break;default:f="Unknown TWPF "+e;break}return f};c.__convertCAP_PRINTERMODE_toString=function(e){var f="";switch(e){case EnumDWT_CapPrinterMode.TWPM_SINGLESTRING:f="Single String";break;case EnumDWT_CapPrinterMode.TWPM_MULTISTRING:f="Multi String";break;case EnumDWT_CapPrinterMode.TWPM_COMPOUNDSTRING:f="Compound String";break;default:f="Unknown TWPM "+e;break}return f};c.__convertCAP_PRINTER_toString=function(e){var f="";switch(e){case EnumDWT_CapPrinter.TWPR_IMPRINTERTOPBEFORE:f="Imprinter Top Before";break;case EnumDWT_CapPrinter.TWPR_IMPRINTERTOPAFTER:f="Imprinter Top After";break;case EnumDWT_CapPrinter.TWPR_IMPRINTERBOTTOMBEFORE:f="Imprinter Bottom Before";break;case EnumDWT_CapPrinter.TWPR_IMPRINTERBOTTOMAFTER:f="Imprinter Bottom After";break;case EnumDWT_CapPrinter.TWPR_ENDORSERTOPBEFORE:f="Endorser Top Before";break;case EnumDWT_CapPrinter.TWPR_ENDORSERTOPAFTER:f="Endorser Top After";break;case EnumDWT_CapPrinter.TWPR_ENDORSERBOTTOMBEFORE:f="Endorser Bottom Before";break;case EnumDWT_CapPrinter.TWPR_ENDORSERBOTTOMAFTER:f="Endorser Bottom After";break;default:f="Unknown TWPR "+e;break}return f};c.__convertCAP_POWERSUPPLY_toString=function(e){var f="";switch(e){case 0:f="External";break;case 1:f="Battery";break;default:f="Unknown TWPS "+e;break}return f};c.__convertICAP_PIXELTYPE_toString=function(e){var f="";switch(e){case EnumDWT_PixelType.TWPT_BW:f="Black & White";break;case EnumDWT_PixelType.TWPT_GRAY:f="Grayscale";break;case EnumDWT_PixelType.TWPT_RGB:f="RGB";break;case EnumDWT_PixelType.TWPT_PALLETE:f="Palette";break;case EnumDWT_PixelType.TWPT_CMY:f="CMY";break;case EnumDWT_PixelType.TWPT_CMYK:f="CMYK";break;case EnumDWT_PixelType.TWPT_YUV:f="YUV";break;case EnumDWT_PixelType.TWPT_YUVK:f="YUVK";break;case EnumDWT_PixelType.TWPT_CIEXYZ:f="CIEXYZ";break;case EnumDWT_PixelType.TWPT_LAB:f="LAB";break;case EnumDWT_PixelType.TWPT_SRGB:f="SRGB";break;case EnumDWT_PixelType.TWPT_SCRGB:f="SCRGB";break;case EnumDWT_PixelType.TWPT_INFRARED:f="INFRARED";break;default:f="Unknown TWPT "+e;break}return f};c.__convertCAP_SEGMENTED_toString=function(e){var f="";switch(e){case 0:f="None";break;case 1:f="Auto";break;default:f="Unknown TWSG "+e;break}return f};c.__convertExtImageInfoName_toString=function(e){var f="";switch(e){case 4608:f="TWEI_BARCODEX";break;case 4609:f="TWEI_BARCODEY";break;case 4610:f="TWEI_BARCODETEXT";break;case 4611:f="TWEI_BARCODETYPE";break;case 4612:f="TWEI_DESHADETOP";break;case 4613:f="TWEI_DESHADELEFT";break;case 4614:f="TWEI_DESHADEHEIGHT";break;case 4615:f="TWEI_DESHADEWIDTH";break;case 4616:f="TWEI_DESHADESIZE";break;case 4617:f="TWEI_SPECKLESREMOVED";break;case 4618:f="TWEI_HORZLINEXCOORD";break;case 4619:f="TWEI_HORZLINEYCOORD";break;case 4620:f="TWEI_HORZLINELENGTH";break;case 4621:f="TWEI_HORZLINETHICKNESS";break;case 4622:f="TWEI_VERTLINEXCOORD";break;case 4623:f="TWEI_VERTLINEYCOORD";break;case 4624:f="TWEI_VERTLINELENGTH";break;case 4625:f="TWEI_VERTLINETHICKNESS";break;case 4626:f="TWEI_PATCHCODE";break;case 4627:f="TWEI_ENDORSEDTEXT";break;case 4629:f="TWEI_FORMCONFIDENCE";break;case 4629:f="TWEI_FORMTEMPLATEMATCH";break;case 4630:f="TWEI_FORMTEMPLATEPAGEMATCH";break;case 4631:f="TWEI_FORMHORZDOCOFFSET";break;case 4632:f="TWEI_FORMVERTDOCOFFSET";break;case 4633:f="TWEI_BARCODECOUNT";break;case 4634:f="TWEI_BARCODECONFIDENCE";break;case 4635:f="TWEI_BARCODEROTATION";break;case 4636:f="TWEI_BARCODETEXTLENGTH";break;case 4637:f="TWEI_DESHADECOUNT";break;case 4638:f="TWEI_DESHADEBLACKCOUNTOLD";break;case 4639:f="TWEI_DESHADEBLACKCOUNTNEW";break;case 4640:f="TWEI_DESHADEBLACKRLMIN";break;case 4641:f="TWEI_DESHADEBLACKRLMAX";break;case 4642:f="TWEI_DESHADEWHITECOUNTOLD";break;case 4643:f="TWEI_DESHADEWHITECOUNTNEW";break;case 4644:f="TWEI_DESHADEWHITERLMIN";break;case 4645:f="TWEI_DESHADEWHITERLAVE";break;case 4646:f="TWEI_DESHADEWHITERLMAX";break;case 4647:f="TWEI_BLACKSPECKLESREMOVED";break;case 4648:f="TWEI_WHITESPECKLESREMOVED";break;case 4649:f="TWEI_HORZLINECOUNT";break;case 4650:f="TWEI_VERTLINECOUNT";break;case 4651:f="TWEI_DESKEWSTATUS";break;case 4652:f="TWEI_SKEWORIGINALANGLE";break;case 4653:f="TWEI_SKEWFINALANGLE";break;case 4654:f="TWEI_SKEWCONFIDENCE";break;case 4655:f="TWEI_SKEWWINDOWX1";break;case 4656:f="TWEI_SKEWWINDOWY1";break;case 4657:f="TWEI_SKEWWINDOWX2";break;case 4658:f="TWEI_SKEWWINDOWY2";break;case 4659:f="TWEI_SKEWWINDOWX3";break;case 4660:f="TWEI_SKEWWINDOWY3";break;case 4661:f="TWEI_SKEWWINDOWX4";break;case 4662:f="TWEI_SKEWWINDOWY4";break;case 4664:f="TWEI_BOOKNAME";break;case 4665:f="TWEI_CHAPTERNUMBER";break;case 4666:f="TWEI_DOCUMENTNUMBER";break;case 4667:f="TWEI_PAGENUMBER";break;case 4668:f="TWEI_CAMERA";break;case 4669:f="TWEI_FRAMENUMBER";break;case 4670:f="TWEI_FRAME";break;case 4671:f="TWEI_PIXELFLAVOR";break;case 4677:f="TWEI_PAGESIDE";break;case 4675:f="TWEI_MAGDATA";break;case 4676:f="TWEI_MAGTYPE";break;default:f="ExtImageInfo ID "+e;break}return f};c.__convertICAP_SUPPORTEDSIZES_toString=function(e){var f="";switch(e){case EnumDWT_CapSupportedSizes.TWSS_NONE:f="None";break;case EnumDWT_CapSupportedSizes.TWSS_A4:f="A4";break;case EnumDWT_CapSupportedSizes.TWSS_JISB5:f="JIS B5";break;case EnumDWT_CapSupportedSizes.TWSS_USLETTER:f="US Letter";break;case EnumDWT_CapSupportedSizes.TWSS_USLEGAL:f="US Legal";break;case EnumDWT_CapSupportedSizes.TWSS_A5:f="A5";break;case EnumDWT_CapSupportedSizes.TWSS_ISOB4:f="ISO B4";break;case EnumDWT_CapSupportedSizes.TWSS_ISOB6:f="ISO B6";break;case EnumDWT_CapSupportedSizes.TWSS_USLEDGER:f="US Ledger";break;case EnumDWT_CapSupportedSizes.TWSS_USEXECUTIVE:f="US Executive";break;case EnumDWT_CapSupportedSizes.TWSS_A3:f="A3";break;case EnumDWT_CapSupportedSizes.TWSS_ISOB3:f="ISO B3";break;case EnumDWT_CapSupportedSizes.TWSS_A6:f="A6";break;case EnumDWT_CapSupportedSizes.TWSS_C4:f="C4";break;case EnumDWT_CapSupportedSizes.TWSS_C5:f="C5";break;case EnumDWT_CapSupportedSizes.TWSS_C6:f="C6";break;case EnumDWT_CapSupportedSizes.TWSS_4A0:f="4A0";break;case EnumDWT_CapSupportedSizes.TWSS_2A0:f="2A0";break;case EnumDWT_CapSupportedSizes.TWSS_A0:f="A0";break;case EnumDWT_CapSupportedSizes.TWSS_A1:f="A1";break;case EnumDWT_CapSupportedSizes.TWSS_A2:f="A2";break;case EnumDWT_CapSupportedSizes.TWSS_A7:f="A7";break;case EnumDWT_CapSupportedSizes.TWSS_A8:f="A8";break;case EnumDWT_CapSupportedSizes.TWSS_A9:f="A9";break;case EnumDWT_CapSupportedSizes.TWSS_A10:f="A10";break;case EnumDWT_CapSupportedSizes.TWSS_ISOB0:f="ISO B0";break;case EnumDWT_CapSupportedSizes.TWSS_ISOB1:f="ISO B1";break;case EnumDWT_CapSupportedSizes.TWSS_ISOB2:f="ISO B2";break;case EnumDWT_CapSupportedSizes.TWSS_ISOB5:f="ISO B5";break;case EnumDWT_CapSupportedSizes.TWSS_ISOB7:f="ISO B7";break;case EnumDWT_CapSupportedSizes.TWSS_ISOB8:f="ISO B8";break;case EnumDWT_CapSupportedSizes.TWSS_ISOB9:f="ISO B9";break;case EnumDWT_CapSupportedSizes.TWSS_ISOB10:f="ISO B10";break;case EnumDWT_CapSupportedSizes.TWSS_JISB0:f="JIS B0";break;case EnumDWT_CapSupportedSizes.TWSS_JISB1:f="JIS B1";break;case EnumDWT_CapSupportedSizes.TWSS_JISB2:f="JIS B2";break;case EnumDWT_CapSupportedSizes.TWSS_JISB3:f="JIS B3";break;case EnumDWT_CapSupportedSizes.TWSS_JISB4:f="JIS B4";break;case EnumDWT_CapSupportedSizes.TWSS_JISB6:f="JIS B6";break;case EnumDWT_CapSupportedSizes.TWSS_JISB7:f="JIS B7";break;case EnumDWT_CapSupportedSizes.TWSS_JISB8:f="JIS B8";break;case EnumDWT_CapSupportedSizes.TWSS_JISB9:f="JIS B9";break;case EnumDWT_CapSupportedSizes.TWSS_JISB10:f="JIS B10";break;case EnumDWT_CapSupportedSizes.TWSS_C0:f="C0";break;case EnumDWT_CapSupportedSizes.TWSS_C1:f="C1";break;case EnumDWT_CapSupportedSizes.TWSS_C2:f="C2";break;case EnumDWT_CapSupportedSizes.TWSS_C3:f="C3";break;case EnumDWT_CapSupportedSizes.TWSS_C7:f="C7";break;case EnumDWT_CapSupportedSizes.TWSS_C8:f="C8";break;case EnumDWT_CapSupportedSizes.TWSS_C9:f="C9";break;case EnumDWT_CapSupportedSizes.TWSS_C10:f="C10";break;case EnumDWT_CapSupportedSizes.TWSS_USSTATEMENT:f="US Statement";break;case EnumDWT_CapSupportedSizes.TWSS_BUSINESSCARD:f="Business Card";break;case EnumDWT_CapSupportedSizes.TWSS_MAXSIZE:f="MAX size";break;default:f="Unknown TWSS "+e;break}return f};c.__convertICAP_XFERMECH_toString=function(e){var f="";switch(e){case EnumDWT_TransferMode.TWSX_NATIVE:f="Native";break;case EnumDWT_TransferMode.TWSX_FILE:f="File";break;case EnumDWT_TransferMode.TWSX_MEMORY:f="Memory";break;case EnumDWT_TransferMode.TWSX_MEMFILE:f="Memory of File";break;default:f="Unknown TWSX "+e;break}return f};c.__convertICAP_UNITS_toString=function(e){var f="";switch(e){case EnumDWT_UnitType.TWUN_INCHES:f="Inches";break;case EnumDWT_UnitType.TWUN_CENTIMETERS:f="Centimeters";break;case EnumDWT_UnitType.TWUN_PICAS:f="Picas";break;case EnumDWT_UnitType.TWUN_POINTS:f="Points";break;case EnumDWT_UnitType.TWUN_TWIPS:f="Twips";break;case EnumDWT_UnitType.TWUN_PIXELS:f="Pixels";break;case EnumDWT_UnitType.TWUN_MILLIMETERS:f="Millimeters";break;default:f="Unknown TWUN "+e;break}return f}}a.DynamicLoadAddonFuns.push(b)})(Dynamsoft.Lib);
