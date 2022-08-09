<template>
  <v-row>
    <v-col>
      <invoice-table>

      </invoice-table>
    </v-col>
  </v-row>
</template>

<script>
import InvoiceTable from '~/components/Invoices/InvoiceTable.vue'
export default {
    name: "InvoicesPage",
    components: { InvoiceTable },
}
</script>

<style>

</style>