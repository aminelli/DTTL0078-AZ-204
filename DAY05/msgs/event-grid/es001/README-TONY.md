# Step Integrazione Event Grid


## Creazione Certificati etc.

```powershell

# Creazione certificato
step ca init --deployment-type standalone --name MqttAppSamplesCA --dns localhost --address 127.0.0.1:443 --provisioner MqttAppSamplesCAProvisioner

# Note: Pwd: Corso2025!

# Output Comando:
# ✔ Root certificate: C:\Users\AMinelli\.step\certs\root_ca.crt
# ✔ Root private key: C:\Users\AMinelli\.step\secrets\root_ca_key
# ✔ Root fingerprint: a22f678ce92a16f42ca93eddb63a22aff65f389c0e77905e62c87b262e0c4283
# ✔ Intermediate certificate: C:\Users\AMinelli\.step\certs\intermediate_ca.crt
# ✔ Intermediate private key: C:\Users\AMinelli\.step\secrets\intermediate_ca_key
# ✔ Database folder: C:\Users\AMinelli\.step\db
# ✔ Default configuration: C:\Users\AMinelli\.step\config\defaults.json
# ✔ Certificate Authority configuration: C:\Users\AMinelli\.step\config\ca.json
# 
# Your PKI is ready to go. To generate certificates for individual services see 'step help ca'.
# 
# FEEDBACK 😍 🍻
#   The step utility is not instrumented for usage statistics. It does not phone
#   home. But your feedback is extremely valuable. Any information you can provide
#   regarding how you’re using `step` helps. Please send us a sentence or two,
#   good or bad at feedback@smallstep.com or join GitHub Discussions
#   https://github.com/smallstep/certificates/discussions and our Discord
#   https://u.step.sm/discord.


# Creazione certificato Client1
step certificate create client1-authn-ID client1-authn-ID.pem client1-authn-ID.key --ca C:\Users\AMinelli\.step\certs\intermediate_ca.crt --ca-key C:\Users\AMinelli\.step\secrets\intermediate_ca_key --no-password --insecure --not-after 2400h
# Visualizzazione del thumbprint
step certificate fingerprint client1-authn-ID.pem
# Output: 4b06b424bd46b3d84d679f212539897eed7165028d84c30890a3e03f4e0e141f


# Creazione certificato Client2
step certificate create client2-authn-ID client2-authn-ID.pem client2-authn-ID.key --ca C:\Users\AMinelli\.step\certs\intermediate_ca.crt --ca-key C:\Users\AMinelli\.step\secrets\intermediate_ca_key --no-password --insecure --not-after 2400h
# Visualizzazione del thumbprint
step certificate fingerprint client2-authn-ID.pem
# Output: 6574b6bc518d555154109829d7a9c94e9cef278261bb5fc7b8de5aec9a584978

```

- A questo punto creare un servzio in azure Event Grid Namespaces; es. Servizio: client1egndttl0078
- Abilitare il servizio MQTT
- Registrare i clients e relativi certificati
- Registrare il namespaces
- Registrare le permission per i publisher
- Registrare le permissione per i subscriber


Creazione Client MQTT: MQTTX (Docker):

```powershell

# CLI
docker pull emqx/mqttx-cli
docker run -it --rm emqx/mqttx-cli

# WEB APP
docker pull emqx/mqttx-web
docker run -d --name mqttx-web -p 8080:80 emqx/mqttx-web


```