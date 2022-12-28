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
with open('Response.txt', 'w') as f:
    f.write(response)
with open('Response.txt', 'r') as f:
    UnityEngine.Debug.Log(f.read())
    print (f.read())
UnityEngine.Debug.Log("FinishPy")
