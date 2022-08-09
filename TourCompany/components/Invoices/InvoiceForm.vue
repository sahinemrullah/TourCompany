<template>
  <v-card>
    <v-card-title>
      <span class="text-h5">{{ formTitle }}</span>
    </v-card-title>

    <v-card-text>
      <v-container>
        <v-stepper v-model="step" vertical>
          <v-stepper-step step="1" :rules="stepValues[0].rules">
            Select tourist.
            <small v-if="!stepValues[0].isValid">
              {{ stepValues[0].errorMessage }}
            </small>
          </v-stepper-step>

          <v-stepper-content step="1">
            <tourist-table
              :show-select="true"
              :single-select="true"
              :selected-tourists="[tourist]"
              class="mb-6"
              @on-select="$emit('update:tourist', $event)"
            >
            </tourist-table>
            <v-btn text @click="step = step - 1"> Previous </v-btn>
            <v-btn color="primary" @click="getUnpaidBookingsByTourist(tourist.touristID); step = step + 1"> Next </v-btn>
          </v-stepper-content>

          <v-stepper-step step="2" :rules="stepValues[1].rules">
            Select bookings.
            <small v-if="!stepValues[1].isValid">
              {{ stepValues[1].errorMessage }}
            </small>
          </v-stepper-step>

          <v-stepper-content step="2">
            <v-container>
              <v-row class="mb-3">
                <v-card class="mx-auto">
                  <v-list flat subheader>
                    <v-subheader>Unpaid Bookings</v-subheader>

                    <v-list-item-group
                      :value="bookings.map(b => b.idx)"
                      multiple
                      @change="$emit('update:bookings', $event.map(e => { return { idx: e, booking: unpaidBookings[e]} }))"
                    >
                      <v-list-item v-for="(item, idx) in unpaidBookings" :key="idx">
                        <template #default="{ active }">
                          <v-list-item-action>
                            <v-checkbox :input-value="active"></v-checkbox>
                          </v-list-item-action>

                          <v-list-item-content>
                            <v-list-item-title>{{ item.name }} {{ item.Date }}</v-list-item-title>
                          </v-list-item-content>
                        </template>
                      </v-list-item>
                    </v-list-item-group>
                  </v-list>
                </v-card>
              </v-row>
              <v-row>
                <v-btn class="mx-3" @click="step = step - 1"> Previous </v-btn>
                <v-btn
                  :color="isValid ? 'primary' : 'error'"
                  @click="
                    $emit('on-save', isValid)
                    step = isValid ? 1 : step
                  "
                >
                  Submit
                </v-btn>
              </v-row>
            </v-container>
          </v-stepper-content>
        </v-stepper>
      </v-container>
    </v-card-text>
  </v-card>
</template>
<script>
import { mapActions, mapGetters } from 'vuex'
import TouristTable from '../Tourists/TouristTable.vue'

export default {
  components: { TouristTable },
  props: {
    formTitle: {
      type: String,
      default: 'Title',
    },
    tourist: {
      type: Object,
      default: () => null,
    },
    bookings: {
      type: Array,
      default: () => [],
    },
  },
  emits: ['on-save', 'on-close', 'update:tourist', 'update:bookings'],
  data() {
    return {
      step: 1,
      stepValues: [
        {
          isValid: false,
          errorMessage: null,
          rules: [() => !!this.tourist || 'You need to select tourist.'],
        },
        {
          isValid: false,
          errorMessage: null,
          rules: [() => (this.bookings.length > 0) || 'You need to select at least 1 booking.',],
        },
      ],
    }
  },
  computed: {
    ...mapGetters({
      unpaidBookings: 'invoices/getUnpaidBookings',
    }),
    isValid() {
      this.stepValues.forEach((v) => {
        v.isValid = v.rules.every((validate) => {
          const validation = validate()
          if (validation === true) return true
          v.errorMessage = validation
          return false
        })
      })
      return this.stepValues.every((v) => v.isValid)
    },
  },
  methods: {
    ...mapActions({
      getUnpaidBookingsByTourist: 'invoices/getUnpaidBookingsByTourist',
    }),
  },
}
</script>