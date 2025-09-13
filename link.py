import os
from openai import OpenAI

client = OpenAI(
    api_key="sk-9e1ae410ccf64117bd53a2d01e8b7c08",
    base_url="https://dashscope.aliyuncs.com/compatible-mode/v1",
)
completion = client.chat.completions.create(
    model="qwen-vl-plus", 
    messages=[{"role": "user","content": [
            {"type": "text","text": "描述一下图片用英文"},
            {"type": "image_url",
             "image_url": {"url": "https://dashscope.oss-cn-beijing.aliyuncs.com/images/dog_and_girl.jpeg"}}
            ]}]
    )
print(completion.model_dump_json())
