import os, sys, logging, time, datetime, queue, threading, traceback
import argparse,json
from subprocess import Popen, PIPE
import subprocess
import shutil

logger = logging.getLogger(__name__)
INFLUXDB_HOST="127.0.0.1"
INFLUXDB_PORT=8086
INFLUXDB_USERNAME="root"
INFLUXDB_PASSWORD="root"
INFLUXDB_DB_NAME="corepro_history_property"

def is_blank(s):
    return not bool(s and s.strip())

def is_valid_date(date_str:str):
    return True

def delete_data(database:str, measurement:str, start_date:str, end_date:str,
                user_name:str, password:int):
    
    success =False
    
    where_statement = ""

    if not is_blank(start_date) and  is_blank(end_date):
        where_statement="where time >'{}'".format(start_date)
    elif not is_blank(start_date) and not is_blank(end_date):
        where_statement= "where time >'{}' and time <'{}'".format(start_date, end_date)
    elif  is_blank(start_date) and not is_blank(end_date):
        where_statement= "where time < '{}' ".format(end_date)
    execute_influx_cmd="delete from {} {}".format(measurement, where_statement)
    cmd = [
        "influx",
        "-database",
        database,
        "-username",
        user_name,
        "-password",
        password,
        "-execute",
        execute_influx_cmd
    ]
    
    logger.info("cmd: {}".format(cmd))
    process = subprocess.Popen(cmd, stdout=PIPE, stderr=PIPE)
    output,error = process.communicate() 
    logger.debug('output : {}'.format(output))
    if process.returncode != 0:
        logger.debug('error : {}'.format(error))        
    else:
        success=True
        
    return success, error 

if __name__ == "__main__":
        
    logging.basicConfig(
        level=logging.DEBUG,
        format='%(asctime)s %(levelname)s %(filename)s(%(lineno)d) %(message)s'
    )
    INFLUXDB_PASSWORD
    parser = argparse.ArgumentParser()
    parser.add_argument('--action', dest='action', type=str, default='delete', help="The action to operator influxdb")
    parser.add_argument('--username', dest='username', type=str, default=INFLUXDB_USERNAME, help="The user of influx db")
    parser.add_argument('--password', dest='password', type=str, default=INFLUXDB_PASSWORD, help="The host of influx db")
    parser.add_argument('--host', dest='host', type=str, default=INFLUXDB_HOST, help="The host of influx db")
    parser.add_argument('--port', dest='port', type=int, default=INFLUXDB_PORT, help="The port of influx db")
    parser.add_argument('--database', dest='database', type=str, default=INFLUXDB_DB_NAME, help="The target database of influx db")
    parser.add_argument('--measurement', dest='measurement', type=str, default='', help="The measurement of some database of influxdb")
    parser.add_argument('--start_date', dest='start_date', type=str, default='', help="The start date option")
    parser.add_argument('--end_date', dest='end_date', type=str, default='', help="The end date option")
    args = parser.parse_args()

    action = args.action
    if action=='delete_data':
        if is_blank(args.measurement):
            logger.info("measurement can't be empty")
            os._exit(-2)
        delete_data(args.database, args.measurement, args.start_date, args.end_date, args.username, args.password)
    else:
        logger.info("not supported action({})".format(action))
