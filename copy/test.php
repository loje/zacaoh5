<?php
	include "TopSdk.php";
	date_default_timezone_set('Asia/Shanghai'); 

	$appkey = '23561086';
	$secret = '437f13b84774200c534e9eda63a41558';
	$c = new TopClient;
	$c ->appkey = $appkey ;
	$c ->secretKey = $secret ;
	$req = new AlibabaAliqinFcSmsNumSendRequest;
	$req ->setExtend( "Y" );
	$req ->setSmsType( "normal" );
	$req ->setSmsFreeSignName( "杂草君" );
	$req ->setSmsParam( "{name:'韩叶墨',company:'启凡汇',datetime:'2016-12-19',address:'深圳市南山区同发路T6艺术区一楼A12',job:'活动策划',money:'100'}" );
	$req ->setRecNum( "13480867001" );
	$req ->setSmsTemplateCode( "SMS_34615318" );
	$resp = $c ->execute( $req );
	echo "<pre>";print_r($resp);echo "</pre>";
?>