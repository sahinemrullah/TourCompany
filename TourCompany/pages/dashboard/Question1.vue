<template>
  <v-card>
  <v-switch v-model="most" :label="`${(most ? 'En çok' : 'En az')} ziyaret edilen bölge`">
  </v-switch>
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
    name: "Question1Page",
    data() {
      return {
        loading: true,
        most: true,
        headers: [
          { text: "Bölge No", value: "destinationID" },
          { text: "Bölge Adı", value: "name" },
          { text: "Ziyaret Sayısı", value: "count" },
        ],
        items: []
      }
    },
    async fetch() {
      this.items = await this.$axios.$get(`/dashboard/visitedPlaces?most=${this.most}`)
      this.loading = false;
    },
    watch: {
      most() {
        this.loading = true;
        this.$axios.$get(`/dashboard/visitedPlaces?most=${this.most}`).then(response => {
          this. items = response
          this.loading = false;
        })
      }
    },
}
</script>

<style>

</style>