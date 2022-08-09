<template>
  <v-row>
    <v-col>
      <v-data-table
        :headers="headers"
        :items="bookings"
        sort-by="bookingID"
        :server-items-length="bookingCount"
        :loading="loading"
        :options.sync="options"
        class="elevation-1"
        @update:options="options = $event"
      >
        <template #[`top`]>
          <v-toolbar flat>
            <v-toolbar-title>Bookings</v-toolbar-title>
            <v-divider class="mx-4" inset vertical></v-divider>
            <v-spacer></v-spacer>
            <v-dialog v-model="dialog">
              <template #[`activator`]="{ on, attrs }">
                <v-btn
                  color="primary"
                  dark
                  class="mb-2"
                  v-bind="attrs"
                  v-on="on"
                >
                  Add Booking
                </v-btn>
              </template>
              <booking-form
                :tour="editedItem.tour"
                :guide="editedItem.guide"
                :tourists="editedItem.tourists"
                :tour-date="editedItem.date"
                :form-title="formTitle"
                @on-save="save"
                @on-close="close"
                @update:tour="editedItem.tour = $event[0]"
                @update:guide="editedItem.guide = $event[0]"
                @update:tourists="editedItem.tourists = $event"
                @update:date="editedItem.date = $event"
              >
              </booking-form>
            </v-dialog>
            <v-dialog v-model="dialogDelete" max-width="500px">
              <v-card>
                <v-card-title class="text-center"
                  >Bu rehberi silmek istediğinize emin misiniz?</v-card-title
                >
                <v-card-actions>
                  <v-spacer></v-spacer>
                  <v-btn color="blue darken-1" @click="closeDelete"
                    >Cancel</v-btn
                  >
                  <v-btn color="blue darken-1" text @click="deleteItemConfirm"
                    >Delete</v-btn
                  >
                  <v-spacer></v-spacer>
                </v-card-actions>
              </v-card>
            </v-dialog>
          </v-toolbar>
        </template>
        <template #[`item.date`]="{ item }">
          <span>{{ item.date.split('T')[0] }}</span>
        </template>
        <template #[`item.guide`]="{ item }">
          <span>{{ `${item.guide.name} ${item.guide.surname}` }}</span>
        </template>
        <template #[`item.actions`]="{ item }">
          <v-menu bottom left>
            <template #activator="{ on, attrs }">
              <v-btn icon v-bind="attrs" v-on="on">
                <v-icon>mdi-dots-vertical</v-icon>
              </v-btn>
            </template>

            <v-list>
              <v-list-item>
                <v-list-item-title
                  ><v-btn text color="primary" @click="editItem(item)"
                    ><v-icon>mdi-pencil</v-icon> Edit</v-btn
                  ></v-list-item-title
                >
              </v-list-item>
              <v-list-item>
                <v-list-item-title
                  ><v-btn text color="error" @click="deleteItem(item)"
                    ><v-icon>mdi-delete</v-icon> Delete</v-btn
                  ></v-list-item-title
                >
              </v-list-item>
            </v-list>
          </v-menu>
        </template>
      </v-data-table>
    </v-col>
  </v-row>
</template>

<script>
import { mapActions, mapGetters } from 'vuex'
import BookingForm from '~/components/Bookings/BookingForm.vue'
export default {
  name: 'BookingsPage',
  components: { BookingForm },
  data() {
    return {
      loading: true,
      options: {
        page: 1,
        itemsPerPage: 10,
      },
      dialog: false,
      dialogDelete: false,
      headers: [
        { text: 'No', value: 'bookingID' },
        { text: 'Name', value: 'tour.name' },
        { text: 'Date', value: 'date', sort: (a, b) => Date.parse(a) - Date.parse(b) },
        { text: 'Guide', value: 'guide' },
        { text: 'Actions', value: 'actions', sortable: false },
      ],
      editedIndex: -1,
      editedItem: {
        tour: null,
        guide: null,
        tourists: [],
        date: new Date().toISOString().split('T')[0]
      },
      defaultItem: {
        tour: null,
        guide: null,
        tourists: [],
        date: new Date().toISOString().split('T')[0]
      },
    }
  },
  computed: {
    ...mapGetters({
      genders: 'getGenders',
      bookings: 'bookings/getBookings',
      bookingCount: 'bookings/getBookingCount',
    }),
    formTitle() {
      return this.editedIndex === -1 ? 'New Booking' : 'Edit Booking'
    },
  },
  watch: {
    dialog(val) {
      val || this.close()
    },
    dialogDelete(val) {
      val || this.closeDelete()
    },
    options: {
      handler() {
        this.loading = true
        this.getBookings(this.options).then((data) => {
          this.loading = false
        })
      },
      deep: true,
    },
  },
  methods: {
    ...mapActions({
      postBooking: 'bookings/postBooking',
      getBookings: 'bookings/getBookings',
      editBooking: 'bookings/editBooking',
    }),
    editItem(item) {
      this.editedIndex = this.bookings.indexOf(item)
      this.editedItem = {
        bookingID: item.bookingID,
        tour: item.tour,
        guide: item.guide,
        tourists: item.tourists,
        date: item.date
      }
      this.dialog = true
    },

    deleteItem(item) {
      this.editedIndex = this.bookings.indexOf(item)
      this.editedItem = {
        bookingID: item.bookingID,
        tour: item.tour,
        guide: item.guide,
        tourists: item.tourists,
        date: item.date
      }
      this.dialogDelete = true
    },
    deleteItemConfirm() {
      this.bookings.splice(this.editedItem, this.editedIndex)
      this.closeDelete()
    },

    close() {
      this.dialog = false
      this.$nextTick(() => {
        this.editedItem = Object.assign({}, this.defaultItem)
        this.editedIndex = -1
      })
    },

    closeDelete() {
      this.dialogDelete = false
      this.$nextTick(() => {
        this.editedItem = Object.assign({}, this.defaultItem)
        this.editedIndex = -1
      })
    },
    save(e) {
      if (e === true) {
        if (this.editedIndex > -1) {
          this.editBooking({ booking: this.editedItem, idx: this.editedIndex})
        } else {
          this.postBooking(this.editedItem)
        }
        this.close()
      }
    },
  },
}
</script>

<style>
</style>