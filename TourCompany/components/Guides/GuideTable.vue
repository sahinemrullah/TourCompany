<template>
  <v-row>
    <v-col>
      <v-data-table
        :headers="headers"
        :items="guides"
        sort-by="guideID"
        item-key="guideID"
        :show-select="showSelect"
        :single-select="singleSelect"
        :value="!guide ? [] : [guide]"
        :server-items-length="guideCount"
        :loading="loading"
        :options.sync="options"
        class="elevation-1"
        @update:options="options = $event"
        @input="$emit('on-select', $event)"
      >
        <template #[`top`]>
          <v-toolbar flat>
            <v-toolbar-title>Guides</v-toolbar-title>
            <v-divider class="mx-4" inset vertical></v-divider>
            <v-spacer></v-spacer>
            <v-dialog v-model="dialog" max-width="500px">
              <template #[`activator`]="{ on, attrs }">
                <v-btn
                  color="primary"
                  dark
                  class="mb-2"
                  v-bind="attrs"
                  v-on="on"
                >
                  Add Guide
                </v-btn>
              </template>
              <guide-form
                :name="editedItem.name"
                :surname="editedItem.surname"
                :telephone-number="editedItem.telephoneNumber"
                :gender="editedItem.gender"
                :form-title="formTitle"
                :selected-languages="editedItem.languages"
                @on-save="save"
                @on-close="close"
                @update:name="editedItem.name = $event"
                @update:surname="editedItem.surname = $event"
                @update:telephoneNumber="editedItem.telephoneNumber = $event"
                @update:gender="editedItem.gender = $event"
                @update:languages="editedItem.languages = $event"
              >
              </guide-form>
            </v-dialog>
            <v-dialog v-model="dialogDelete" max-width="500px">
              <v-card>
                <v-card-title class="text-center"
                  >Bu rehberi silmek istediğinize emin misiniz?</v-card-title
                >
                <v-card-actions>
                  <v-spacer></v-spacer>
                  <v-btn color="blue darken-1" text @click="closeDelete"
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
    <v-snackbar
      v-model="errorSnackBar"
    >
      {{ errorMessage }}

      <template #:action="{ attrs }">
        <v-btn
          color="blue"
          text
          v-bind="attrs"
          @click="errorSnackBar = false"
        >
          Close
        </v-btn>
      </template>
    </v-snackbar>
  </v-row>
</template>

<script>
import { mapActions, mapGetters } from 'vuex'
import GuideForm from '~/components/Guides/GuideForm.vue'
export default {
  name: 'GuidesPage',
  components: { GuideForm },
  props: {
    guide: {
      type: Object,
      default: () => {},
    },
    showSelect: {
      type: Boolean,
      default: false,
    },
    singleSelect: {
      type: Boolean,
      default: true,
    },
  },
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
        { text: 'Guide No', value: 'guideID' },
        { text: 'Name', value: 'name' },
        { text: 'Surname', value: 'surname' },
        { text: 'Gender', value: 'gender.name' },
        { text: 'Telephone Number', value: 'telephoneNumber' },
        { text: 'Actions', value: 'actions', sortable: false },
      ],
      editedIndex: -1,
      editedItem: {
        name: '',
        surname: '',
        gender: null,
        telephoneNumber: '',
        languages: [],
      },
      defaultItem: {
        name: '',
        surname: '',
        gender: null,
        telephoneNumber: '',
        languages: [],
      },
      errorMessage: '',
      errorSnackBar: false,
    }
  },
  computed: {
    ...mapGetters({
      genders: 'getGenders',
      guides: 'guides/getGuides',
      guideCount: 'guides/getGuideCount',
    }),
    formTitle() {
      return this.editedIndex === -1 ? 'New Guide' : 'Edit Guide'
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
        this.getGuides(this.options).then((data) => {
          this.loading = false
        })
      },
      deep: true,
    },
  },
  methods: {
    ...mapActions({
      postGuide: 'guides/postGuide',
      getGuides: 'guides/getGuides',
      editGuide: 'guides/editGuide',
      deleteGuide: 'guides/deleteGuide',
    }),
    editItem(item) {
      this.editedIndex = this.guides.indexOf(item)
      this.editedItem = Object.assign({}, item)
      this.dialog = true
    },

    deleteItem(item) {
      this.editedIndex = this.guides.indexOf(item)
      this.editedItem = Object.assign({}, item)
      this.dialogDelete = true
    },
    deleteItemConfirm() {
      this.deleteGuide({guide: this.editedItem, idx: this.editedIndex}).then().catch(err => {
        this.errorSnackBar = true
        this.errorMessage = err
      })
      this.dialogDelete = false;
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
        if (this.editedItem.gender == null)
          this.editedItem.gender = this.genders[0]
        if (this.editedIndex > -1) {
          this.editGuide({ guide: this.editedItem, idx: this.editedIndex })
        } else {
          this.postGuide(this.editedItem)
        }
        this.close()
      }
    },
  },
}
</script>

<style>
</style>