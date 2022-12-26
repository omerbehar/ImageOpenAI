import os
import openai
import json
import UnityEngine

UnityEngine.Debug.Log("StartPy")
openai.api_key = "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"
prompt = "create a funny sentence using the words apples and snakes"
prompt = "give me a complement"
completion = openai.Completion.create(model="text-davinci-003", prompt=prompt, max_tokens=10, temperature=0.9).to_dict()
response = completion['choices'][0]['text']
response = str.replace(response, "\n", "")
print(response)
#UnityEngine.Debug.Log(response)
with open('Response.txt', 'w') as f:
    f.write(response)
with open('Response.txt', 'r') as f:
    UnityEngine.Debug.Log(f.__dir__())
    UnityEngine.Debug.Log(f.read())

UnityEngine.Debug.Log("FinishPy")
