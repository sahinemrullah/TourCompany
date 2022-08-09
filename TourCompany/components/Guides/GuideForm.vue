<template>
  <v-card>
    <v-card-title>
      <span class="text-h5">{{ formTitle }}</span>
    </v-card-title>

    <v-card-text>
      <v-container>
        <v-form ref="form">
          <v-row>
            <v-col cols="12" sm="6" md="6">
              <v-text-field
                :value="name"
                label="Name"
                :rules="nameRules"
                @input="$emit('update:name', $event)"
              ></v-text-field>
            </v-col>
            <v-col cols="12" sm="6" md="6">
              <v-text-field
                :value="surname"
                label="Surname"
                :rules="surnameRules"
                @input="$emit('update:surname', $event)"
              ></v-text-field>
            </v-col>
          </v-row>
          <v-row>
            <v-col cols="12">
              <v-text-field
                :value="telephoneNumber"
                label="Telephone Number"
                type="number"
                :maxlength="10"
                :rules="telephoneNumberRules"
                @input="$emit('update:telephoneNumber', $event)"
              >
              </v-text-field>
            </v-col>
          </v-row>
          <v-row>
            <v-col cols="12">
              <v-radio-group
                column
                label="Gender"
                :value="genders[0].genderID"
                @change="$emit('update:gender', genders.filter(g => g.genderID === $event)[0])"
              >
                <v-radio
                  v-for="item in genders"
                  :key="item.genderID"
                  :label="item.name"
                  :value="item.genderID"
                ></v-radio>
              </v-radio-group>
            </v-col>
          </v-row>
          <v-row>
            <v-select
              :items="languages"
              label="Select"
              multiple
              chips
              :rules="languagesRules"
              hint="Select the languages that the guide knows."
              persistent-hint
              prepend-icon="mdi-translate"
              item-text="name"
              item-value="languageID"
              :value="selectedLanguages"
              @input="$emit('update:languages', $event)"
            >
            </v-select>
          </v-row>
        </v-form>
      </v-container>
    </v-card-text>

    <v-card-actions>
      <v-spacer></v-spacer>
      <v-btn color="warning" text @click="$emit('on-close')"> Cancel </v-btn>
      <v-btn color="primary" @click="$emit('on-save', validate())"> Save </v-btn>
    </v-card-actions>
  </v-card>
</template>

<script>
import { mapGetters } from 'vuex'
export default {
  props: {
    name: {
      type: String,
      default: "",
    },
    surname: {
      type: String,
      default: "",
    },
    telephoneNumber: {
      type: String,
      default: "",
    },
    formTitle: {
      type: String,
      required: true,
    },
    selectedLanguages: {
        type: Array,
        default: () => []
    }
  },
  emits: [
    'on-close',
    'on-save',
    'update:name',
    'update:surname',
    'update:telephoneNumber',
    'update:gender',
    'update:languages',
  ],
  data() {
    return {
      nameRules: [
        (v) => !!v || 'Name field is required.',
        (v) =>
          (!!v && v.length <= 20) || 'Name cannot be longer than 20 characters',
      ],
      surnameRules: [
        (v) => !!v || 'Surname field is required.',
        (v) =>
          (!!v && v.length <= 40) ||
          'Surname cannot be longer than 40 characters',
      ],
      telephoneNumberRules: [
        (v) => !!v || 'Telephone Number field is required.',
        (v) =>
          (!!v && v.length === 10) ||
          'Telephone number field should be 10 character.',
        (v) =>
          (!!v && v.includes('.') === false) ||
          "Telephone number field can't contain ',' or '.' character.",
      ],
      languagesRules: [
        (v) => (v !== null && v.length !== 0) || 'Languages field is required.',
      ],
    }
  },
  computed: {
    ...mapGetters({
        genders: 'getGenders',
        languages: 'getLanguages',
    })
  },
  methods: {
    validate() {
        return this.$refs.form.validate()
    },
  }
}
</script>

<style>
</style>