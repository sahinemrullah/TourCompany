<template>
<v-card>
  <v-card-title>
    <v-select
    v-model="selectedDestination"
    label="Bölge"
    :items="destinations"
    item-text="name"
    item-value="name">
    </v-select>
  </v-card-title>
  <v-data-table
    :headers="headers"
    :items="items"
    :loading="loading"
    class="elevation-1"
  >
  <template #[`item.birthDate`]="{ item }">
    {{ item.birthDate.split('T')[0] }}
  </template>
  </v-data-table>
</v-card>
</template>

<script>
import { mapGetters, mapActions } from 'vuex'
export default {
    name: "Question8Page",
    data() {
      return {
        headers: [
          { text: "Turist", value: "fullName" },
          { text: "Doğum Günü", value: "birthDate" },
          { text: "Yaş", value: "age" },
        ],
        items: [],
        selectedDestination: "Kız Kulesi",
        loading: true,
      }
    },
    async fetch() {
      this.items = (await this.$axios.$get('/dashboard/VisitorByAge'))
      this.loading = false
    },
    computed: {
      ...mapGetters({
        destinations: 'destinations/getDestinations'
      })
    },
    watch: {
      selectedDestination() {
        this.loading = true
        this.$axios.$get(`/dashboard/VisitorByAge?destinationName=${this.selectedDestination}`).then(response => {
          this.items = response
          this.loading = false
        })
      }
    },
    created() {
      this.getDestinations({page: 1, itemsPerPage: 999})
    },
    methods: {
      ...mapActions({
        getDestinations: 'destinations/getDestinations'
      })
    }
}
</script>

<style>

</style>