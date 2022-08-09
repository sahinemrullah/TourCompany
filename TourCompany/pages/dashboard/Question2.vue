<template>
<v-card>
<v-card-title>
  <v-select
  v-model="selectedMonth"
  label="Ay"
  :items=months
  item-text="name"
  item-value="value">
  </v-select>
</v-card-title>
  <v-data-table
    :headers="headers"
    :items="items"
    :loading="loading"
    class="elevation-1"
  >
  </v-data-table>
</v-card>
</template>

<script>
export default {
    name: "Question2Page",
    data() {
      return {
        headers: [
          { text: "Yıl", value: "year" },
          { text: "Rehber", value: "fullName" },
          { text: "Çalışma Sayısı", value: "bookingCount" },
        ],
        items: [],
        months: [
          {
          name: 'Ocak',
          value: 1,
        },
          {
          name: 'Şubat',
          value: 2,
        },
          {
          name: 'Mart',
          value: 3,
        },
          {
          name: 'Nisan',
          value: 4,
        },
          {
          name: 'Mayıs',
          value: 5,
        },
          {
          name: 'Haziran',
          value: 6,
        },
          {
          name: 'Temmuz',
          value: 7,
        },
          {
          name: 'Ağustos',
          value: 8,
        },
          {
          name: 'Eylül',
          value: 9,
        },
          {
          name: 'Ekim',
          value: 10,
        },
          {
          name: 'Kasım',
          value: 11,
        },
          {
          name: 'Aralık',
          value: 12,
        },
        ],
        selectedMonth: 8,
        loading: true,
      }
    },
    async fetch() {
      this.items = await this.$axios.$get('/dashboard/hardestWorkingGuidesByYear')
      this.loading = false
    },
    watch: {
      selectedMonth() {
        this.loading = true
        this.$axios.$get(`/dashboard/hardestWorkingGuidesByYear?month=${this.selectedMonth}`).then(response => {
          this.items = response
          this.loading = false
        })
      }
    },
}
</script>

<style>

</style>