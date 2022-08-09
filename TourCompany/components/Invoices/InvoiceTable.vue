<template>
  <v-row>
    <v-col>
      <v-data-table
        :headers="headers"
        :items="invoices"
        :single-select="singleSelect"
        :show-select="showSelect"
        :value="selectedInvoices"
        sort-by="invoiceID"
        class="elevation-1"
        item-key="invoiceID"
        :server-items-length="invoiceCount"
        :loading="loading"
        :options.sync="options"
        @update:options="options = $event"
        @input="$emit('on-select', $event)"
      >
        <template #[`top`]>
          <v-toolbar flat>
            <v-toolbar-title>{{ title }}</v-toolbar-title>
            <v-divider class="mx-4" inset vertical></v-divider>
            <v-spacer></v-spacer>
            <v-text-field
              v-model="search"
              label="Search"
              single-line
              hide-details
              @keydown.enter="getInvoices({ options: options, search: search })"
            ></v-text-field
            ><v-btn
              color="primary"
              class="mb-2"
              text
              @click="getInvoices({ options: options, search: search })"
            >
              <v-icon>mdi-magnify</v-icon>
            </v-btn>
            <v-spacer></v-spacer>
            <v-dialog v-model="dialog">
              <template #[`activator`]="{ on, attrs }">
                <v-btn color="primary" class="mb-2" v-bind="attrs" v-on="on">
                  Add Invoice
                </v-btn>
              </template>
              <v-card>
                <invoice-form
                  :tourist="invoice.tourist"
                  :bookings="invoice.bookings"
                  :form-title="formTitle"
                  @on-save="save"
                  @on-close="close"
                  @update:tourist="invoice.tourist = $event[0]"
                  @update:bookings="invoice.bookings = $event"
                >
                </invoice-form>
              </v-card>
            </v-dialog>
          </v-toolbar>
        </template>
        <template #[`item.totalPrice`]="{ item }">
          {{ item.totalPrice }} ₺
        </template>
        <template #[`item.show`]="{ item }">
          <v-btn text @click="showInvoice(item)">
            <v-icon>mdi-magnify</v-icon> View
          </v-btn>
        </template>
      </v-data-table>
      <v-dialog :value="viewInvoice">
        <invoice-view-dialog
        v-if="viewedInvoice != null"
        :invoice="viewedInvoice">
        </invoice-view-dialog>
        <v-btn class="white--text" color="teal" @click="viewInvoice = false">
          Close
        </v-btn>
      </v-dialog >
    </v-col>
  </v-row>
</template>

<script>
import { mapActions, mapGetters } from 'vuex'
import InvoiceViewDialog from './InvoiceViewDialog.vue'
import InvoiceForm from '~/components/Invoices/InvoiceForm.vue'
export default {
  name: 'InvoicesPage',
  components: { InvoiceForm, InvoiceViewDialog },
  props: {
    showSelect: {
      type: Boolean,
      default: false,
    },
    singleSelect: {
      type: Boolean,
      default: false,
    },
    selectedInvoices: {
      type: Array,
      default: () => [],
    },
  },
  emits: ['on-select'],
  data() {
    return {
      viewInvoice: false,
      search: '',
      loading: true,
      options: {
        page: 1,
        itemsPerPage: 10,
      },
      title: 'Invoices',
      headers: [
        { text: 'Invoice No', value: 'invoiceNo' },
        { text: 'Tourist', value: 'tourist' },
        { text: 'Total Price', value: 'totalPrice' },
        { text: '', value: 'show' },
      ],
      editedIndex: -1,
      invoice: {
        tourist: null,
        bookings: [],
      },
      dialog: false,
      defaultInvoice: {
        tourist: null,
        bookings: [],
      },
    }
  },
  computed: {
    ...mapGetters({
      invoices: 'invoices/getInvoices',
      invoiceCount: 'invoices/getInvoiceCount',
      viewedInvoice: 'invoices/getInvoice',
    }),
    formTitle() {
      return this.editedIndex === -1 ? 'New Invoice' : 'Edit Invoice'
    },
  },
  watch: {
    dialog(val) {
      val || this.close()
    },
    options: {
      handler() {
        this.loading = true
        this.getInvoices({ options: this.options, search: this.search }).then(
          (data) => {
            this.loading = false
          }
        )
      },
      deep: true,
    },
  },
  methods: {
    ...mapActions({
      addInvoice: 'invoices/addInvoice',
      getInvoices: 'invoices/getInvoices',
      getInvoiceDetails: 'invoices/getInvoiceDetails',
    }),
    showInvoice(item) {
      this.getInvoiceDetails(item.invoiceNo)
      this.viewInvoice = true
    },
    close() {
      this.dialog = false
      this.$nextTick(() => {
        this.invoice = Object.assign({}, this.defaultInvoice)
        this.editedIndex = -1
      })
    },
    save(e) {
      if (e === true) {
        this.addInvoice(this.invoice).then((res) => {
          this.dialog = false
          this.getInvoices({ options: this.options, search: this.search })
        })
      }
    },
  },
}
</script>

<style>
			
</style>