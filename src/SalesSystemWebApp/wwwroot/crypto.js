async function encryptData(data, keyBase64) {
    try {
        const keyBytes = Uint8Array.from(atob(keyBase64), c => c.charCodeAt(0));
        const key = await crypto.subtle.importKey(
            "raw",
            keyBytes,
            { name: "AES-GCM" },
            false,
            ["encrypt"]
        );

        const dataBytes = new TextEncoder().encode(data);
        const iv = crypto.getRandomValues(new Uint8Array(12)); 

        const encrypted = await crypto.subtle.encrypt(
            { name: "AES-GCM", iv: iv },
            key,
            dataBytes
        );

        const result = new Uint8Array(iv.length + encrypted.byteLength);
        result.set(iv, 0);
        result.set(new Uint8Array(encrypted), iv.length);

        return btoa(String.fromCharCode(...result));
    } catch (error) {
        throw error;
    }
}

async function decryptData(encryptedBase64, keyBase64) {
    try {
        const keyBytes = Uint8Array.from(atob(keyBase64), c => c.charCodeAt(0));
        const key = await crypto.subtle.importKey(
            "raw",
            keyBytes,
            { name: "AES-GCM" },
            false,
            ["decrypt"]
        );

        const encryptedBytes = Uint8Array.from(atob(encryptedBase64), c => c.charCodeAt(0));
        const iv = encryptedBytes.slice(0, 12); 
        const cipherText = encryptedBytes.slice(12);

        const decrypted = await crypto.subtle.decrypt(
            { name: "AES-GCM", iv: iv },
            key,
            cipherText
        );

        return new TextDecoder().decode(decrypted);
    } catch (error) {
        throw error;
    }
}