//
// Dynamsoft JavaScript Library for Basic Initiation of Dynamic Web TWAIN
// More info on DWT: http://www.dynamsoft.com/Products/WebTWAIN_Overview.aspx
//
// Copyright 2016, Dynamsoft Corporation 
// Author: Dynamsoft Team
// Version: 12.1
//
/// <reference path="dynamsoft.webtwain.initiate.js" />
var Dynamsoft = Dynamsoft || { WebTwainEnv: {} };

///
Dynamsoft.WebTwainEnv.AutoLoad = true;
///
Dynamsoft.WebTwainEnv.Containers = [{ContainerId:'dwtcontrolContainer', Width:'560px', Height:'600px'}];

///A9B27B55EF3144EA486A67364D89E617CAD8C658E65E41FEC904845E837AA80EA64A31B71253D81D679DD297A3561AD07B98F94987788A67F1AE35A44410FD123261BBEF72806D72899D8252ECFB2AE03C93A5CEFE622E95A6744AD8EF312366259589832CCE2DD60F3F0EEA 
///9FDDF1C1B4D916684F493C906B439D685C5C0B8FA0D8D944AF1BB007129D72CE521E97602033D436AF1CA3E9D20EFF4CA52F1605BC73D0CB1B6CEE3EE1D9ED8394F94ADEF5672632C95D436EF4C6AD1E512E1E8BECAAB3922007F2D9778D3F5980DA22986A3509E415130F09 
///6D10C5FD4265305616AC2050EDD0B471ED00BF1CD7510DEC498BEE4A6751BCC56BFB124C678CDF90ACFCD55241609478C618A3D8C048327D2A455D48F6948081F3E25B07A5D36A6A7DA7D2C0EF3C25B9E1CCFC1AECAACADF03DFF5962B3E6D3B21F19F58B2040ED20E770EDF
//Dynamsoft.WebTwainEnv.ProductKey = '1A21FC678C0104BE32B7ED1789FCE71EC24FF1D4ACD4C5C0C804047BC7977BAFDB9BAE0605220AF9CF8017988FEA43BE3028E15ED9B6F33D9C954D4600ED454CD7F06BECA08A7A0FA990092C2ADA0FADC8AA78B784F5FEF055A962F7711A537F3B0E12A208E568CE6C37AEB346E38C3E9285A90700E8E46912BD06D286D7470DEA0546DC674DA8363D3A12360885EFD6DEB20A4974929E7EA5931C678520F497535914F868B7DF88C5772F79FFADC16164437EBE4D7F7A44D93CF73CE7E788E94DE6CE2BEF2196AA7DB6CFC04AEA754D9CEAB93431040060A9C07B4FD618EFC775B1F8373FD0F4CA8BA49EC4A8A4F979BC8CB82AC6C79A5FF297493070195F229C3328346623B14DA986402CB7EB46BC4101D59EE38D503F3FBF813E2983ABA8EC5F26E6D94D367D1CFBF7B729AB3F4E889CBDEADED8DD096546B058277C0E93FEBFF4681951C3EEF1B91E2D7AB8D5353E9232DAA960A7EA87CAFA642EAED9EFFB5C16EEF8E6D13118D3204832F7D48D668DE6B069D7C596139AF3BDC0AB44B69BEBC6813C9B65128CB2B4BD75614F67AB6A3E463EDAD81A6B1357748E811E760FEAF0D12944A9D0F90F29698E3D';
//Dynamsoft.WebTwainEnv.ProductKey = 'F6AFE4292C8C104D8AC37588C74439F6B1FEED31676A8708DA0256D89395D87359D9C425D712711965E315295688B05F315EAA7904AACCF1B9730741E7D79560C7E317D16F3318677516F7B1D1270D72ECE7C8889C026E789DE699FC80FD80A5371FC3FF88507E6265333DB358F43220DEE4CD3BCC86C8C580614FD5FC0613334325C489C9A42B8F64E206048934A177BD5A4DFF15EB6882FCBEFFC379AA30CDE443B97777F67E178AD34625D0E5AAB9E08BDB59DE4FF9DD5047B8D58EC1EEED9532CC4E1ABBFFB1311239027E4B5072706E4D24D230F1E4933A99CE3B8C555403F13115EE1784965ECE027A9ED5D7BF966E246EDF8C4F9FC33A21AAA7ACECCBDC8A6A16B84BCF59C1525F9B0715F7BBDE879853923FFEB5183B58AA0DB77E133FD0E5029665B7A40148226D3E2F4B51C518A88BDAFBB93896563225A52942FBDB85E2C3AA9FD56E67C93DF3BF3092D8C2BD279A4B5DCF633E234A0E096F0E287E43E285A0D54EA1C9BB4F610D7199C31908439B46DC8A817A5E4F5F58109D81BED53D0502B030AC5AA06FB16C29B39861F45368E05E78EE8002EC9D5EC26374EC2F420F6397BADED39EFF563E8E58';
//Dynamsoft.WebTwainEnv.ProductKey = 'A9B27B55EF3144EA486A67364D89E617CAD8C658E65E41FEC904845E837AA80EA64A31B71253D81D679DD297A3561AD07B98F94987788A67F1AE35A44410FD123261BBEF72806D72899D8252ECFB2AE03C93A5CEFE622E95A6744AD8EF312366259589832CCE2DD60F3F0EEA';
//Dynamsoft.WebTwainEnv.ProductKey = '9FDDF1C1B4D916684F493C906B439D685C5C0B8FA0D8D944AF1BB007129D72CE521E97602033D436AF1CA3E9D20EFF4CA52F1605BC73D0CB1B6CEE3EE1D9ED8394F94ADEF5672632C95D436EF4C6AD1E512E1E8BECAAB3922007F2D9778D3F5980DA22986A3509E415130F09';
//Dynamsoft.WebTwainEnv.ProductKey = '6D10C5FD4265305616AC2050EDD0B471ED00BF1CD7510DEC498BEE4A6751BCC56BFB124C678CDF90ACFCD55241609478C618A3D8C048327D2A455D48F6948081F3E25B07A5D36A6A7DA7D2C0EF3C25B9E1CCFC1AECAACADF03DFF5962B3E6D3B21F19F58B2040ED20E770EDF';
Dynamsoft.WebTwainEnv.ProductKey = 'FA6C62C327E2B820D70F11D68B40D79C8E9A0F67F513BFE6B2684A8A80F658664B441FC0A952D138EDF7925A37F0194DDB2A5E253D612284641D54357A3772EC146A4F32B1C9FFEB2D7B0917767F48D70808DD7BEF333E47AC954FECC43C5ADB8E08FD6619043B5B11620EF5';
///
Dynamsoft.WebTwainEnv.Trial = true;

///
Dynamsoft.WebTwainEnv.ActiveXInstallWithCAB = false;

///
Dynamsoft.WebTwainEnv.ResourcesPath = '../Scripts/Resources';

/// All callbacks are defined in the dynamsoft.webtwain.install.js file, you can customize them.
// Dynamsoft.WebTwainEnv.RegisterEvent('OnWebTwainReady', function(){
// 		// webtwain has been inited
// });

