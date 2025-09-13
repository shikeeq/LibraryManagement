#include <WiFi.h>
#include <HTTPClient.h>

// Wi-Fi设置
const char* ssid = "wmx";              // 你的Wi-Fi名称
const char* password = "wmx2590750240"; // 你的Wi-Fi密码

// Dashscope API的URL（请替换为您使用的实际URL）
const char* apiUrl = "https://dashscope.aliyuncs.com/compatible-mode/v1/chat/completions";// 改成你的api url

// 设置HTTP客户端
HTTPClient http;

void setup() {
  // 启动串口
  Serial.begin(115200);
  delay(1000);

  // 连接到Wi-Fi
  Serial.print("Connecting to WiFi");
  WiFi.begin(ssid, password);
  while (WiFi.status() != WL_CONNECTED) {
    delay(1000);
    Serial.print(".");
  }
  Serial.println("Connected to WiFi");

  // 启动HTTP客户端
  http.begin(apiUrl);  // 设置API的URL
  http.addHeader("Content-Type", "application/json");  // 设置请求头
  http.addHeader("Authorization", "Bearer sk-9e1ae410ccf64117bd53a2d01e8b7c08");  // 设置Bearer认证头 填写你的密钥

  // 设置请求数据（JSON格式） 改成你的输入文本（英文）和图片http链接
  String requestBody = "{\"model\":\"qwen-vl-plus\", \"messages\":[{\"role\":\"user\",\"content\":[{\"type\":\"text\",\"text\":\"请描述一下图片的内容\"},{\"type\":\"image_url\",\"image_url\":{\"url\":\"https://dashscope.oss-cn-beijing.aliyuncs.com/images/dog_and_girl.jpeg\"}}]}]}";

  // 发送POST请求
  int httpResponseCode = http.POST(requestBody);
  
  // 检查HTTP响应码
  if (httpResponseCode > 0) {
    String response = http.getString();  // 获取响应内容
    Serial.println("Response: ");
    Serial.println(response);  // 输出整个响应内容

    // 这里我们只提取JSON中的"content"部分
    int startIdx = response.indexOf("\"content\":\"") + 11;
    int endIdx = response.indexOf("\"", startIdx);
    String content = response.substring(startIdx, endIdx);

    // 输出提取的内容
    Serial.println("Extracted Content: ");
    Serial.println(content);
    
    // 检查是否成功提取到内容
    if (content.length() > 0) {
      Serial.println("Successfully extracted content!");
    } else {
      Serial.println("Failed to extract content from the response.");
    }
  } else {
    // 如果请求失败，打印错误信息
    Serial.print("Error in HTTP request. HTTP Response Code: ");
    Serial.println(httpResponseCode);
  }

  // 关闭HTTP客户端
  http.end();
}

void loop() {
  // 在这里可以执行其他任务
}
