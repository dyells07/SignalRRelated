select is_broker_enabled from sys.databases where name='AjaxCall'

ALTER DATABASE AjaxCall SET ENABLE_BROKER WITH ROLLBACK IMMEDIATE;


