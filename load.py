#!/usr/bin/python2.7

import requests
import random
import json
import time
import math

def proceed_with_probability(threshold):
    if threshold < random.randint(0, 10):
        raise ""

def season_factor():
    season_length = 3 * 60
    timestamp = time.time()
    arg = (timestamp / season_length) * math.pi
    return math.sin(arg)

while True:
    try:
        requests.get("http://localhost:8081/api/tickets")
        proceed_with_probability(7 + season_factor() * 3)
        requests.post("http://localhost:8081/api/tickets/checkout")
        proceed_with_probability(8 + season_factor() * 2)
        requests.post("http://localhost:8081/api/tickets/pay")
    except KeyboardInterrupt:
        exit()
    except:
       pass

