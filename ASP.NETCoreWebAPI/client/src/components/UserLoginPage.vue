<template>
  <div class="login-page">
    <h2>User Login</h2>
    <form @submit.prevent="loginUser">
      <div class="form-group">
        <label for="username">Username</label>
        <input
            type="text"
            id="username"
            v-model="username"
            placeholder="Enter your username"
            required
        />
      </div>
      <div class="form-group">
        <label for="password">Password</label>
        <input
            type="password"
            id="password"
            v-model="password"
            placeholder="Enter your password"
            required
        />
      </div>
      <button type="submit">Login</button>
    </form>

    <div v-if="errorMessage" class="error-message">
      {{ errorMessage }}
    </div>

    <div v-if="successMessage" class="success-message">
      {{ successMessage }}
    </div>
  </div>
</template>

<script>
import axios from 'axios';

export default {
  data() {
    return {
      username: '',
      password: '',
      successMessage: '',
      errorMessage: '',
    };
  },
  methods: {
    async loginUser() {
      const loginRequest = {
        Username: this.username,
        Password: this.password,
      };

      try {
        // API'ye POST isteği gönderiyoruz
        const response = await axios.post('https://localhost:7079/api/User/login', loginRequest);

        if (response.status === 200) {
          // Başarılı giriş mesajı
          this.successMessage = response.data.message;
          this.errorMessage = '';

          // Giriş başarılıysa yönlendir
          this.$router.push('/crud');
        } else {
          this.errorMessage = 'Unexpected response from the server.';
        }
      } catch (error) {
        // Hata mesajı
        if (error.response && error.response.data) {
          this.errorMessage = error.response.data.message || 'An error occurred while logging in.';
        } else {
          this.errorMessage = 'Unable to connect to the server.';
        }
        this.successMessage = '';
      }
    },
  },
};
</script>

<style scoped>
.login-page {
  width: 300px;
  margin: 0 auto;
  padding: 20px;
  border: 1px solid #ccc;
  border-radius: 8px;
  background-color: #f9f9f9;
}

h2 {
  text-align: center;
  margin-bottom: 20px;
}

.form-group {
  margin-bottom: 15px;
}

label {
  display: block;
  margin-bottom: 5px;
}

input {
  width: 100%;
  padding: 8px;
  margin: 5px 0;
  border: 1px solid #ccc;
  border-radius: 4px;
}

button {
  width: 100%;
  padding: 10px;
  background-color: #007bff;
  color: white;
  border: none;
  border-radius: 4px;
  cursor: pointer;
}

button:hover {
  background-color: #0056b3;
}

.error-message {
  color: red;
  margin-top: 10px;
}

.success-message {
  color: green;
  margin-top: 10px;
}
</style>
