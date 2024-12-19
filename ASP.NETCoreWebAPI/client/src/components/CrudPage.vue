<template>
  <div class="crud-page">
    <h1>CRUD İşlemleri</h1>

    <!-- Form Bölümü -->
    <div class="form-container">
      <h2 v-if="isEditing">Veri Güncelle</h2>
      <h2 v-else>Yeni Veri Ekle</h2>

      <form @submit.prevent="isEditing ? updateData() : createData()">
        <div class="form-group">
          <label for="name">Ad:</label>
          <input type="text" id="name" v-model="formData.name" required placeholder="Adınızı girin..." />
        </div>
        <div class="form-group">
          <label for="email">E-posta:</label>
          <input type="email" id="email" v-model="formData.email" required placeholder="E-posta adresinizi girin..." />
        </div>
        <div class="form-group">
          <button type="submit">{{ isEditing ? 'Güncelle' : 'Ekle' }}</button>
        </div>
      </form>
    </div>

    <!-- Veri Listesi -->
    <div class="data-list">
      <h2>Veri Listesi</h2>
      <table>
        <thead>
        <tr>
          <th>Ad</th>
          <th>E-posta</th>
          <th>İşlemler</th>
        </tr>
        </thead>
        <tbody>
        <tr v-for="(item, index) in dataList" :key="index">
          <td>{{ item.name }}</td>
          <td>{{ item.email }}</td>
          <td>
            <button @click="editData(item, index)">Düzenle</button>
            <button @click="deleteData(index)">Sil</button>
          </td>
        </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<script>
export default {
  data() {
    return {
      dataList: [],
      formData: {
        name: '',
        email: ''
      },
      isEditing: false,
      editingIndex: null
    };
  },
  methods: {
    // Veri ekleme
    createData() {
      const newData = { ...this.formData };
      this.dataList.push(newData);
      this.resetForm();
    },

    // Veri güncelleme
    updateData() {
      if (this.editingIndex !== null) {
        this.$set(this.dataList, this.editingIndex, { ...this.formData });
      }
      this.resetForm();
    },

    // Veri düzenleme
    editData(item, index) {
      this.formData.name = item.name;
      this.formData.email = item.email;
      this.isEditing = true;
      this.editingIndex = index;
    },

    // Veri silme
    deleteData(index) {
      this.dataList.splice(index, 1);
    },

    // Formu sıfırlama
    resetForm() {
      this.formData.name = '';
      this.formData.email = '';
      this.isEditing = false;
      this.editingIndex = null;
    }
  }
};
</script>

<style scoped>
/* Genel sayfa stili */
.crud-page {
  max-width: 1200px;
  margin: 20px auto;
  padding: 20px;
  background-color: #f9f9f9;
  border-radius: 8px;
}

/* Başlık */
h1 {
  text-align: center;
  color: #333;
  font-size: 2em;
  margin-bottom: 20px;
}

/* Form container */
.form-container {
  background-color: #fff;
  padding: 20px;
  border-radius: 8px;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
  margin-bottom: 30px;
}

.form-container h2 {
  color: #333;
  margin-bottom: 20px;
}

.form-group {
  margin-bottom: 15px;
}

.form-group label {
  font-size: 1em;
  color: #555;
  display: block;
  margin-bottom: 5px;
}

.form-group input {
  width: 100%;
  padding: 10px;
  font-size: 1em;
  border: 1px solid #ddd;
  border-radius: 5px;
}

.form-group button {
  padding: 10px 15px;
  background-color: #4CAF50;
  color: white;
  border: none;
  border-radius: 5px;
  cursor: pointer;
  font-size: 1em;
}

.form-group button:hover {
  background-color: #45a049;
}

/* Veri listesi */
.data-list {
  background-color: #fff;
  padding: 20px;
  border-radius: 8px;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
}

table {
  width: 100%;
  border-collapse: collapse;
}

table th,
table td {
  padding: 12px;
  text-align: left;
  border: 1px solid #ddd;
}

table th {
  background-color: #4CAF50;
  color: white;
}

table td {
  background-color: #f2f2f2;
}

button {
  padding: 8px 12px;
  margin: 5px;
  cursor: pointer;
  font-size: 0.9em;
}

button:hover {
  background-color: #f44336;
  color: white;
}

/* Responsive Tasarım */
@media (max-width: 768px) {
  .crud-page {
    padding: 10px;
  }

  h1 {
    font-size: 1.8em;
  }

  .form-container {
    padding: 15px;
  }

  .form-group input,
  .form-group button {
    font-size: 0.9em;
  }

  table {
    font-size: 0.9em;
  }

  table th,
  table td {
    padding: 8px;
  }
}
</style>
